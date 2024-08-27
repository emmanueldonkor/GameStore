using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Games.Queries;

public record GetGameByIdQuery(Guid GameId) : IRequest<Game?>
{

}
public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Game?>
{
    private readonly IApplicationDbContext dbContext;

    public GetGameByIdQueryHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Game?> Handle(GetGameByIdQuery request, CancellationToken ct = default)
    {
        return await dbContext.Games
                     .FindAsync([request.GameId, ct], cancellationToken: ct);
    }
}
