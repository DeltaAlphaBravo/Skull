using Skull.Skull.GamesState;
using Skull.Skull.Phases;

namespace Skull.Skull
{
    public class SkullGame : ISkullGame
    {
        private readonly IGameStateRepository _repository;

        public SkullGame(IGameStateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IGameState> CreateGameAsync()
        {
            var newGame = CreationPhase.StartGame("Booyah");
            await _repository.SaveGameStatusAsync(newGame);
            return newGame;
        }

        public async Task<IGameState> StartGame(string game)
        {
            var gameState = await GetGameStateValidForPhase(game, Phase.Creation);
            if (gameState.Players.Count is < 3 or > 6) throw new Exceptions.WrongNumberOfPlayersException();
            gameState.GoToNextPhase();
            return gameState;
        }

        public async Task<IGameState> JoinPlayer(string game, string playerName, bool isSkull)
        {
            var gameState = await GetGameStateValidForPhase(game, Phase.Creation);

            var creationPhase = CreationPhase.CreateFromState(gameState);
            gameState = creationPhase.PlaceFirstCoaster(creationPhase.JoinPlayer(playerName), isSkull);
            if (gameState.Players.All(p => p.PlayerIdentity != null)) gameState.GoToNextPhase();
            await _repository.SaveGameStatusAsync(gameState);
            return gameState;
        }

        public async Task<IGameState?> GetGameStateAsync(string name)
        {
            return await _repository.GetGameStateAsync(name);
        }

        public async Task<IGameState> PlaceCoasterAsync(string game, int player, bool isSkull)
        {
            var gameState = await GetGameStateValidForPhase(game, Phase.Placement);

            var placementPhase = PlacementPhase.CreateFromState(gameState);
            gameState = placementPhase.PlaceCoaster(player, isSkull);
            await _repository.SaveGameStatusAsync(gameState);
            return gameState;
        }

        public async Task<IGameState> MakeBidAsync(string name, int player, int? cardsToReveal)
        {
            var gameState = await _repository.GetGameStateAsync(name);
            if (gameState == null) throw new Exceptions.GameNotFoundException();
            if (gameState.Phase == Phase.Placement) gameState.GoToNextPhase();
            if (gameState.Phase != Phase.Challenge) throw new Exceptions.WrongPhaseException();

            var game = ChallengePhase.CreateFromState(gameState);
            gameState = game.MakeBid(player, cardsToReveal);
            PossiblyGoToNextPhase(cardsToReveal, gameState);
            await _repository.SaveGameStatusAsync(gameState);
            return gameState;
        }

        private async Task<IGameState> GetGameStateValidForPhase(string game, Phase phase)
        {
            var gameState = await _repository.GetGameStateAsync(game);
            if (gameState == null) throw new Exceptions.GameNotFoundException();
            if (gameState.Phase != phase) throw new Exceptions.WrongPhaseException();
            return gameState;
        }

        private static void PossiblyGoToNextPhase(int? cardsToReveal, IGameState gameState)
        {
            int totalPlayedCoasters = gameState.Players.SelectMany(p => p.PlayerState.PlayedCoasters).Count();
            var isMaxPossibleBidPlaced = cardsToReveal > 0 && cardsToReveal == totalPlayedCoasters;
            var isOnlyOnePlayerNotPassing = gameState.Bids.Count(b => b.CardsToReveal == null) == gameState.Players.Count() - 1;
            if (isOnlyOnePlayerNotPassing || isMaxPossibleBidPlaced) gameState.GoToNextPhase();
        }
    }
}
