using System.Data.Common;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Checkout;

public record CreateCheckoutCommand : IRequest<CreateCheckoutResult>
{
    public Guid UserId { get; init; }
    public decimal TotalAmount { get; init; }
    public required List<OrderItemDto> OrderItems { get; init; }
    public required ShippingAddressDto ShippingAddress { get; init; }
    public string? SuccessUrl { get; init; }
    public string? CancelUrl { get; init; }
}

public record CreateCheckoutResult
{
    public string? SessionId { get; init; }
}

public class CreateCheckoutCommandHandler : IRequestHandler<CreateCheckoutCommand, CreateCheckoutResult>
{
    private readonly IApplicationDbContext dbContext;
    private readonly IPaymentService paymentService;

    public CreateCheckoutCommandHandler(IApplicationDbContext dbContext, IPaymentService paymentService)
    {
        this.dbContext = dbContext;
        this.paymentService = paymentService;
    }

    public async Task<CreateCheckoutResult> Handle(CreateCheckoutCommand request, CancellationToken cancellationToken)
    {  
        using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            var newOrder = new Order
            {
                UserId = request.UserId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = request.TotalAmount,
                Status = OrderStatus.Pending,
                OrderItems = request.OrderItems.Select(oi => new OrderItem
                {
                    GameId = oi.GameId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList(),
                ShippingAddress = new ShippingAddress
                {
                    AddressLine = request.ShippingAddress.AddressLine,
                    City = request.ShippingAddress.City,
                    State = request.ShippingAddress.State,
                    ZipCode = request.ShippingAddress.ZipCode,
                    Country = request.ShippingAddress.Country
                }
            };

            dbContext.Orders.Add(newOrder);
            await dbContext.SaveChangesAsync(cancellationToken);

            var session = await paymentService.CreateStripeSessionAsync(newOrder, request.SuccessUrl, request.CancelUrl);

            await transaction.CommitAsync(cancellationToken);

            return new CreateCheckoutResult { SessionId = session.Id };
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
