using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Games.Commands.CreateGame;

public record CreateGameCommand(string Name, string Description, string Category, decimal Price, string ImageUrl) : IRequest<Game>
{

}

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Game>
{
    private readonly IApplicationDbContext dbContext;

    public CreateGameCommandHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async  Task<Game> Handle(CreateGameCommand request, CancellationToken ct = default)
    {
       Game? newGame = new()
       {
        Name = request.Name,
        Description = request.Description,
        Category = request.Category,
        Price = request.Price,
        ImageUrl = request.ImageUrl,
       };
        dbContext.Games.Add(newGame);
        await dbContext.SaveChangesAsync(ct);
        return newGame;
    }
}