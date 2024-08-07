using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IApplicationDbContext dbContext;
    private readonly IConfiguration configuration;

    public PaymentService(IApplicationDbContext context, IConfiguration configuration)
    {
        dbContext = context;
        this.configuration = configuration;
    }

    public async Task<Session> CreateStripeSessionAsync(Order order, string successUrl, string cancelUrl)
    {
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = ["card"],
            LineItems = order.OrderItems?.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "eur",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Game - " + item.GameId,
                    },
                    UnitAmount = (long)(item.Price * 100),
                },
                Quantity = item.Quantity,
            }).ToList(),
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            ClientReferenceId = order.Id.ToString()
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);
        return session;
    }

    public async Task HandlePaymentSuccessAsync(Session session)
    {
        var orderId = Guid.Parse(session.ClientReferenceId);
        var order = await dbContext.Orders.FindAsync(orderId);

        if (order is not null)
        {
            order.Status = OrderStatus.Completed;
            dbContext.Orders.Update(order);

            var payment = new Payment
            {
                OrderId = order.Id,
                PaymentMethod = "card",
                TransactionId = session.PaymentIntentId,
                Amount = order.TotalAmount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Paid
            };

            dbContext.Payments.Add(payment);

            await dbContext.SaveChangesAsync();
        }
    }
}