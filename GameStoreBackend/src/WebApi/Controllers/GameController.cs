using Application.Games.Commands.CreateGame;
using Application.Games.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly ISender sender;

    public GameController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(CreateGameCommand command)
    {
        var game = await sender.Send(command);
        return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(Guid id)
    {
        var query = new GetGameByIdQuery(id);
        var result = await sender.Send(query);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}