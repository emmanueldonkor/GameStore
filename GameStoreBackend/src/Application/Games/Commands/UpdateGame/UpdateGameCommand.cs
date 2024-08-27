using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Games.Commands.UpdateGame;

public record class UpdateGameCommand : IRequest
{
    public Guid GameId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Genre { get; init; }
    public decimal? Price { get; init; }
    public string? ImageUrl { get; init; }
}

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
{
    private readonly IApplicationDbContext context;
    public UpdateGameCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task Handle(UpdateGameCommand request, CancellationToken ct = default)
    {
        var existingGame = await context.Games.FindAsync([request.GameId], cancellationToken: ct);
        if (existingGame is null)
        {
            throw new NotFoundException(nameof(existingGame), request.GameId);
        }
        existingGame.Name = request.Name ?? existingGame.Name;
        existingGame.Description = request.Description ?? existingGame.Description;
        existingGame.Genre = request.Genre ?? existingGame.Genre;
        existingGame.Price = request.Price ?? existingGame.Price;
        existingGame.ImageUrl = request.ImageUrl ?? existingGame.ImageUrl;
        await context.SaveChangesAsync(ct);
    }
}
