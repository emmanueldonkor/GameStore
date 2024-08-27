using Application.Common.Interfaces;
using MediatR;
using Stripe.Checkout;

namespace Application.Payments;

public record class HandlePaymentSuccessCommand : IRequest<Unit>
{
    public required Session Session { get; init; }
}

public class HandlePaymentSuccessCommandHandler : IRequestHandler<HandlePaymentSuccessCommand, Unit>
{
    private readonly IPaymentService paymentService;

    public HandlePaymentSuccessCommandHandler(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    public async Task<Unit> Handle(HandlePaymentSuccessCommand request, CancellationToken cancellationToken)
    {
        await paymentService.HandlePaymentSuccessAsync(request.Session);
        return Unit.Value;
    }
}