using Microsoft.AspNetCore.Mvc;
using Skull.Skull;
using Skull.Skull.GamesState;

namespace Skull.Api.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    private readonly ISkullGame _skullGame;
    private readonly ISkullHub _skullHub;

    public GameController(ISkullGame skullGame, ISkullHub skullHub)
    {
        _skullGame = skullGame;
        _skullHub = skullHub;
    }

    [HttpPost]
    [Route("api/game")]
    public async Task<IGameState> CreateNewGameAsync()
    {
        var gameState = await _skullGame.CreateGameAsync();
        //await _skullHub.AddToGroupAsync(gameState.Name);
        return gameState;
    }
}
