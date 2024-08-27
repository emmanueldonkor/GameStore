using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Checkout;

public record class CreateCheckoutCommand : IRequest<CreateCheckoutResult>
{
    public Guid UserId { get; init; }
    public decimal TotalAmount { get; init; }
    public required List<OrderItemDto> OrderItems { get; init; }
    public required ShippingAddressDto ShippingAddress { get; init; }
    public required string SuccessUrl { get; init; } = "https://example.com/success";
    public required string CancelUrl { get; init; } = "https://example.com/cancel";
}

public record CreateCheckoutResult
{
    public required string SessionId { get; init; }
}

public class CreateCheckoutCommandHandler : IRequestHandler<CreateCheckoutCommand, CreateCheckoutResult>
{
    private readonly IApplicationDbContext dbContext;
    private readonly IPaymentService paymentService;
    private readonly IMapper mapper;

    public CreateCheckoutCommandHandler(IApplicationDbContext dbContext, IPaymentService paymentService, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.paymentService = paymentService;
        this.mapper = mapper;
    }

    public async Task<CreateCheckoutResult> Handle(CreateCheckoutCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            var newOrder = mapper.Map<CreateCheckoutCommand, Order>(request);
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
