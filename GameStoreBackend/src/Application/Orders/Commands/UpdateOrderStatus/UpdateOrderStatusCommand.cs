using Application.Exceptions;
using Application.Interfaces;
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
        if(existingOrder == null)
        {
           Result.Failure($"Order with id {request.OrderId} not found");
        }
       existingOrder.Status = request.Status;
       await dbContext.SaveChangesAsync(cancellationToken);   
    }
}