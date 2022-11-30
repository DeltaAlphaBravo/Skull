using Microsoft.AspNetCore.Mvc;
using Skull.Api.Models;
using Skull.Exceptions;

namespace Skull.Api.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    private readonly ISkullGame _skullGame;
    private readonly ISkullHub _skullHub;
    private readonly ITableRepository _tableRepository;

    public GameController(ISkullGame skullGame, 
                          ISkullHub skullHub, 
                          ITableRepository tableRepository)
    {
        _skullGame = skullGame;
        _skullHub = skullHub;
        _tableRepository = tableRepository;
    }

    [HttpPost]
    [Route("api/table/{tableName}/game")]
    public async Task<ActionResult> CreateNewGameAsync([FromRoute] string tableName)
    {
        try
        {
            var table = await _tableRepository.GetTableAsync(tableName);
            if (table == null) return new NotFoundResult();
            var gameState = await _skullGame.StartGameAsync(table);
            await _skullHub.NotifyGameStarted(tableName);
            return new OkResult();
        }
        catch(WrongNumberOfPlayersException)
        {
            return new BadRequestResult();
        }
    }


    [HttpGet]
    [Route("api/table/{tableName}/player/{player}/view")]
    public async Task<ActionResult<IGamePlayerView>> GetGamePlayerViewAsync([FromRoute] string tableName, [FromRoute] int player)
    {
        var gameState = await _skullGame.GetGameStateAsync(tableName);
        if (gameState == null) return new NotFoundResult();
        var view = new OkObjectResult(new GamePlayerView(gameState, player));
        return view;
    }

    [HttpPost]
    [Route("api/table/{tableName}/player/{player}/stack")]
    public async Task<ActionResult<IGamePlayerView>> PlaceCoasterAsync([FromRoute] string tableName, [FromRoute] int player, [FromBody] bool isSkull)
    {
        try
        {
            var gameState = await _skullGame.PlaceCoasterAsync(tableName, player, isSkull);
            if (gameState == null) return new NotFoundResult();
            await _skullHub.NotifyNewPlacement(tableName, player);
            return new OkObjectResult(new GamePlayerView(gameState, player));
        }
        catch (InvalidOperationException)
        {
            return new BadRequestResult();
        }
    }

    [HttpPost]
    [Route("api/table/{tableName}/player/{player}/challenge")]
    public async Task<ActionResult<IGamePlayerView>> MakeBidAsync([FromRoute] string tableName, [FromRoute] int player, [FromBody] int? bid)
    {
        try
        {
            var gameState = await _skullGame.MakeBidAsync(tableName, player, bid);
            if (gameState == null) return new NotFoundResult();
            await _skullHub.NotifyNewBid(tableName, player, bid);
            return new OkObjectResult(new GamePlayerView(gameState, player));
        }
        catch (InvalidOperationException)
        {
            return new BadRequestResult();
        }
    }
}
