using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Queries;

public record  GetGamesQuery : IRequest<IEnumerable<Game>>
{
    
}

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<Game>>
{
    private readonly IApplicationDbContext dbContext;

    public GetGamesQueryHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async  Task<IEnumerable<Game>> Handle(GetGamesQuery request, CancellationToken ct = default)
    {
        return await dbContext
                    .Games
                    .AsNoTracking()
                    .ToListAsync(ct);
    }
}
