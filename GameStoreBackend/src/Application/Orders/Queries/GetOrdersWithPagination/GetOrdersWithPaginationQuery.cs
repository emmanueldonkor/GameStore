using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersWithPagination;

public record GetOrdersWithPaginationQuery : IRequest<IEnumerable<Order>>
{
    
}

public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, IEnumerable<Order>>
{
    private readonly IApplicationDbContext dbContext;

    public GetOrdersWithPaginationQueryHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Order>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
                        .Include(o => o.OrderItems)
                        .Include(s => s.ShippingAddress)
                        .Include(p => p.Payment)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
    }
}