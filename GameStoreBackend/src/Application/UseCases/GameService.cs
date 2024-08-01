using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases;

public class GameService : IGameService
{
    private readonly IApplicationDbContext dbContext;

    public GameService(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateGameAsync(Game game)
    {
        Game newGame = new()
        {
            Name = game.Name,
            Description = game.Description,
            Category = game.Category,
            Price = game.Price,
            ImageUrl = game.ImageUrl,
            CreatedAt = DateTime.Now
        };
        dbContext.Games.Add(newGame);
        await dbContext.SaveChangesAsync();
    }
    public async Task DeleteGameAsync(Guid id)
    {
        var game = await dbContext.Games.FindAsync(id) ?? throw new Exception("Game not found");
        dbContext.Games.Remove(game);
        await dbContext.SaveChangesAsync();
    }


    public async Task<Game?> GetGameByIdAsync(Guid id)
    {
        return await dbContext.Games.FindAsync(id); 
    }

    public async Task<IEnumerable<Game>> GetGameListAsync()
    {
        return await dbContext.Games.AsNoTracking()
                        .ToListAsync();
        
    }

    public async Task UpdateGameAsync(Game updatedGame)
    {
        var existingGame = await dbContext.Games.FindAsync(updatedGame.Id);
    
        if(existingGame is null)
        throw new NullReferenceException("Game not found");
        existingGame.Name = updatedGame.Name;
        existingGame.Description = updatedGame.Description;
        existingGame.Category = updatedGame.Category;
        existingGame.Price = updatedGame.Price;
        existingGame.ImageUrl = updatedGame.ImageUrl;
        existingGame.CreatedAt = updatedGame.CreatedAt;
        dbContext.Games.Update(updatedGame);
        await dbContext.SaveChangesAsync();
      
    }

}