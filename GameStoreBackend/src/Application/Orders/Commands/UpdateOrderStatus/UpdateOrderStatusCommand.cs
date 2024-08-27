using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands.UpdateOrderStatus;

public record class UpdateOrderStatusCommand : IRequest
{
    public Guid OrderId { get; init; }
    public OrderStatus Status { get; init; }
}

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
{
    private readonly IApplicationDbContext dbContext;

    public UpdateOrderStatusCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var existingOrder = await dbContext.Orders.FindAsync([request.OrderId], cancellationToken: cancellationToken);
        if (existingOrder is null)
        {
           throw new NotFoundException(nameof(existingOrder), request.OrderId);
        }
        existingOrder.Status = request.Status;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}