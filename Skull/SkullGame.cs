using Skull.GamesState;
using Skull.Phases;

namespace Skull
{
    public class SkullGame : ISkullGame
    {
        private readonly IGameStateRepository _repository;

        public SkullGame(IGameStateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IGameState> StartGameAsync(ITable table)
        {
            var playerCount = table.Players.Count;
            if (playerCount is < 3 or > 6) throw new Exceptions.WrongNumberOfPlayersException();
            var gameState = new GameState(playerCount);
            await _repository.SaveGameStateAsync(table.Name, gameState);
            return gameState;
        }

        public async Task<IGameState?> GetGameStateAsync(string name)
        {
            return await _repository.GetGameStateAsync(name);
        }

        public async Task<IGameState> PlaceCoasterAsync(string tableName, int player, bool isSkull)
        {
            var gameState = await GetGameStateValidForPhase(tableName, Phase.Placement);

            var placementPhase = PlacementPhase.CreateFromState(gameState);
            gameState = placementPhase.PlaceCoaster(player, isSkull);
            await _repository.SaveGameStateAsync(tableName, gameState);
            return gameState;
        }

        public async Task<IGameState> MakeBidAsync(string tableName, int player, int? cardsToReveal)
        {
            var gameState = await _repository.GetGameStateAsync(tableName);
            if (gameState == null) throw new Exceptions.GameNotFoundException();
            if (gameState.Phase == Phase.Placement) gameState.GoToNextPhase();
            if (gameState.Phase != Phase.Challenge) throw new Exceptions.WrongPhaseException();

            var game = ChallengePhase.CreateFromState(gameState);
            gameState = game.MakeBid(player, cardsToReveal);
            PossiblyGoToNextPhase(cardsToReveal, gameState);
            await _repository.SaveGameStateAsync(tableName, gameState);
            return gameState;
        }

        private async Task<IGameState> GetGameStateValidForPhase(string tableName, Phase phase)
        {
            var gameState = await _repository.GetGameStateAsync(tableName);
            if (gameState == null) throw new Exceptions.GameNotFoundException();
            if (gameState.Phase != phase) throw new Exceptions.WrongPhaseException();
            return gameState;
        }

        private static void PossiblyGoToNextPhase(int? cardsToReveal, IGameState gameState)
        {
            int totalPlayedCoasters = gameState.PlayerStates.SelectMany(p => p.PlayedCoasters).Count();
            var isMaxPossibleBidPlaced = cardsToReveal > 0 && cardsToReveal == totalPlayedCoasters;
            var isOnlyOnePlayerNotPassing = gameState.Bids.Count(b => b.CardsToReveal == null) == gameState.PlayerStates.Count() - 1;
            if (isOnlyOnePlayerNotPassing || isMaxPossibleBidPlaced) gameState.GoToNextPhase();
        }
    }
}
