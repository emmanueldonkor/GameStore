using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersWithPagination;

public record GetOrdersWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderDto>>
{
    private readonly IApplicationDbContext dbContext;
    private readonly IMapper mapper;

    public GetOrdersWithPaginationQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<PaginatedList<OrderDto>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
                        .Include(o => o.OrderItems)
                        .Include(s => s.ShippingAddress)
                        .Include(p => p.Payment)
                        .OrderBy(o => o.OrderDate)
                        .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);

    }
}