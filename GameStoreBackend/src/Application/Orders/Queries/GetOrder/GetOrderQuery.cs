
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrder;

public record GetOrderQuery : IRequest<Order>
{
    public Guid OrderId { get; init; }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
{
    private readonly  IApplicationDbContext dbContext;

    public GetOrderQueryHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order =  await dbContext.Orders
                .Include(o => o.OrderItems)
                .Include(s => s.ShippingAddress)
                .Include(p => p.Payment)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        if(order is null)
        {
            throw new Exception("Order not found");
        }
        return order;
    }
}