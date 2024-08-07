using Domain.Entities;
using Stripe.Checkout;
namespace Application.Interfaces;

public interface IPaymentService
{
    Task<Session> CreateStripeSessionAsync(Order order, string successUrl, string cancelUrl);
    Task HandlePaymentSuccessAsync(Session session);
}