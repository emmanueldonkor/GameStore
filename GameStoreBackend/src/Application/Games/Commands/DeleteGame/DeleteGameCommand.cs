using Application.Interfaces;
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

    public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
       var existingGame = await  dbContext.Games
                                   .FindAsync([request.GameId], cancellationToken: cancellationToken);

        if(existingGame is null)
        {
            throw new NullReferenceException("Game not found");
        }
        dbContext.Games.Remove(existingGame);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
