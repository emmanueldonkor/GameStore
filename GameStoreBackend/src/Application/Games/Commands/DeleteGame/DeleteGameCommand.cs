using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Games.Commands.DeleteGame;

public record DeleteGameCommand(Guid GameId) : IRequest
{

}
public class DeleteCommandHandler : IRequestHandler<DeleteGameCommand>
{
    private readonly IApplicationDbContext dbContext;

    public DeleteCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Handle(DeleteGameCommand request, CancellationToken ct)
    {
        var existingGame = await dbContext.Games.
            FindAsync([request.GameId], cancellationToken: ct);
        if (existingGame is null)
        {
            throw new NotFoundException(nameof(existingGame), request.GameId);
        }
        dbContext.Games.Remove(existingGame);
        await dbContext.SaveChangesAsync(ct);
    }
}
