using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrdersWithPagination
{
    public record class GetOrdersByIdWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
    {
       public int PageNumber { get; init; } = 1;
       public  int PageSize { get; init; } = 10;
       public required string UserId { get; init; }
    }

    public class GetOrdersByIdWithPaginationHandler : IRequestHandler<GetOrdersByIdWithPaginationQuery, PaginatedList<OrderDto>>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public GetOrdersByIdWithPaginationHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<OrderDto>> Handle(GetOrdersByIdWithPaginationQuery request, CancellationToken ct)
        {
          return await dbContext.Orders
                     .Where(id => id.Auth0UserId == request.UserId)
                     .Include(o => o.OrderItems)
                     .Include(s => s.ShippingAddress)
                     .Include(p => p.Payment)
                     .OrderBy(o => o.OrderDate)
                     .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                     .PaginatedListAsync(request.PageNumber, request.PageSize); 
        }
    }
}