using Skull.GamesState;

namespace Skull.Phases
{
    internal sealed class CreationPhase : ICreationPhase
    {
        public IGameState GameState { get; private set; }
        private CreationPhase(IGameState gameState)
        {
            GameState = gameState;
        }

        public static IGameState StartGame(string name, int playerCount)
        {
            if (playerCount is < 3 or > 6) throw new ArgumentException();
            var players = Enumerable.Range(0, playerCount)
                                    .Select(y => new Player(y))
                                    .ToList<IPlayer>();
            var gameState = new GameState("Booyah", players);
            return gameState;
        }

        public int JoinPlayer(string name)
        {
            return GameState.JoinPlayer(name);
        }

        public IGameState PlaceFirstCoaster(int player, bool isSkull)
        {
            var playerState = isSkull switch
            {
                true => GameState.Players.Single(p => p.PlayerId == player).PlaySkull(),
                false => GameState.Players.Single(p => p.PlayerId == player).PlayFlower()
            };
            return GameState;
        }

        public static ICreationPhase CreateFromState(IGameState gameStatus)
        {
            if (gameStatus.Phase != Phase.Creation) throw new InvalidOperationException();
            return new CreationPhase(gameStatus);
        }
    }
}
