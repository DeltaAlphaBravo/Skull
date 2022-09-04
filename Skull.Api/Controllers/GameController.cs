using Microsoft.AspNetCore.Mvc;
using Skull.Skull;
using Skull.Skull.GamesState;

namespace Skull.Api.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    private readonly ISkullGame _skullGame;

    public GameController(ISkullGame skullGame) => _skullGame = skullGame;

    [HttpPost]
    [Route("api/game")]
    public async Task<IGameState> CreateNewGameAsync()
    {
        return await _skullGame.CreateGameAsync();
    }
}
