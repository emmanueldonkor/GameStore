using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Games.Queries;

public record GetGamesWithPaginationQuery : IRequest<PaginatedList<GameDto>>
{
  public int PageNumber { get; init; } = 1;
  public int PageSize { get; init; } = 10;
}

public class GetGamesQueryHandler : IRequestHandler<GetGamesWithPaginationQuery, PaginatedList<GameDto>>
{
    private readonly IApplicationDbContext dbContext;
    private readonly IMapper mapper;

    public GetGamesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<PaginatedList<GameDto>> Handle(GetGamesWithPaginationQuery request, CancellationToken ct = default)
    {
        return await dbContext.Games
                    .OrderBy(g => g.CreatedAt)
                     .ProjectTo<GameDto>(mapper.ConfigurationProvider)
                     .PaginatedListAsync(request.PageNumber, request.PageSize);
                                    
    }
}
