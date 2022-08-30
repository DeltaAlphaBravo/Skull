﻿using Microsoft.AspNetCore.Mvc;
using SkullApi.Models.External;
using SkullApi.Models.Internal;

namespace SkullApi.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ISkullGame _skullGame;

        public PlayerController(ISkullGame skullGame) => _skullGame = skullGame;

        [HttpGet]
        [Route("api/game/{game}/{player}/view")]
        public async Task<ActionResult<IGamePlayerView>> GetGamePlayerViewAsync([FromRoute] string game, [FromRoute] int player)
        {
            var gameState = await _skullGame.GetGameStateAsync(game);
            if (gameState == null) return new NotFoundResult();
            var view = new OkObjectResult(new GamePlayerView(gameState, player));
            return view;
        }

        [HttpPost]
        [Route("api/game/{game}/{player}/stack")]
        public async Task<ActionResult<IGamePlayerView>> PlaceCoasterAsync([FromRoute] string game, [FromRoute] int player, [FromBody] bool isSkull)
        {
            try
            {
                var gameState = await _skullGame.PlaceCoasterAsync(game, player, isSkull);
                if (gameState == null) return new NotFoundResult();
                return new OkObjectResult(new GamePlayerView(gameState, player));
            }
            catch (InvalidOperationException)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("api/game/{game}/{player}/challenge")]
        public async Task<ActionResult<IGamePlayerView>> MakeBidAsync([FromRoute] string game, [FromRoute] int player, [FromBody] int? bid)
        {
            try
            {
                var gameState = await _skullGame.MakeBidAsync(game, player, bid);
                if (gameState == null) return new NotFoundResult();
                return new OkObjectResult(new GamePlayerView(gameState, player));
            }
            catch (InvalidOperationException)
            {
                return new BadRequestResult();
            }
        }
    }
}
