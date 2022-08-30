using SkullApi.Models.Internal.GamesState;
using SkullApi.Models.Internal.Phases;

namespace SkullApi.Models.Internal
{
    public class SkullGame : ISkullGame
    {
        private readonly IGameStateRepository _repository;

        public SkullGame(IGameStateRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IGameState> CreateGameAsync(int playerCount)
        {
            var newGame = new PlacementPhase(playerCount);
            await _repository.SaveGameStatusAsync(newGame.GameState);
            return newGame.GameState;
        }

        public async Task<IGameState?> GetGameStateAsync(string name)
        {
            return await _repository.GetGameStateAsync(name);
        }

        public async Task<IGameState?> PlaceCoasterAsync(string name, int player, bool isSkull)
        {
            var gameState = await _repository.GetGameStateAsync(name);
            if (gameState == null) return null;
            if (gameState.Phase == Phase.Placement)
            {
                var game = PlacementPhase.CreateFromState(gameState);
                gameState = game.PlaceCoaster(player, isSkull);
                await _repository.SaveGameStatusAsync(gameState);
                return gameState;
            }
            return null;
        }

        public async Task<IGameState?> MakeBidAsync(string name, int player, int? cardsToReveal)
        {
            var gameState = await _repository.GetGameStateAsync(name);
            if (gameState == null) return null;
            if (gameState.Phase == Phase.Placement) gameState.NextPhase();
            if (gameState.Phase == Phase.Challenge)
            {
                var game = ChallengePhase.CreateFromState(gameState);
                gameState = game.MakeBid(player, cardsToReveal);
                PossiblyGoToNextPhase(cardsToReveal, gameState);
                await _repository.SaveGameStatusAsync(gameState);
                return gameState;
            }
            return null;
        }

        private static void PossiblyGoToNextPhase(int? cardsToReveal, IGameState gameState)
        {
            var isOnlyOnePlayerNotPassing = gameState.Bids.Count(b => b.CardsToReveal == null) == gameState.Players.Count() - 1;
            var isMaxPossibleBidPlaced = cardsToReveal > 0 && cardsToReveal == gameState.Players.SelectMany(p => p.PlayedCoasters).Count();
            if (isOnlyOnePlayerNotPassing || isMaxPossibleBidPlaced) gameState.NextPhase();
        }
    }
}
