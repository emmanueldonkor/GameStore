using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Stripe.Checkout;

namespace Application.Payments;

public record class CreateStripeSessionCommand : IRequest<Session>
{
    public required Order Order { get; set; }
    public required string SuccessUrl { get; set; }
    public required string CancelUrl { get; set; }
}

public class CreateStripeSessionCommandHandler : IRequestHandler<CreateStripeSessionCommand, Session>
{
    private readonly IPaymentService paymentService;

    public CreateStripeSessionCommandHandler(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    public async Task<Session> Handle(CreateStripeSessionCommand request, CancellationToken cancellationToken)
    {
        return await paymentService.CreateStripeSessionAsync(request.Order, request.SuccessUrl, request.CancelUrl);
    }
}