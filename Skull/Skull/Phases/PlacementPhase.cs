using Skull.Exceptions;
using Skull.GamesState;

namespace Skull.Phases;

internal sealed class PlacementPhase : IPlacementPhase
{
    public IGameState GameState { get; private init; }

    public PlacementPhase(int playerCount)
    {
        if (playerCount is < 3 or > 6) throw new ArgumentException();
        var players = Enumerable.Range(0, playerCount)
                                .Select(y => new Player(y))
                                .ToList<IPlayer>();
        GameState = new GameState("Booyah", players);
    }

    private PlacementPhase(IGameState gameState)
    {
        GameState = gameState;
    }

    public static IPlacementPhase CreateFromState(IGameState gameStatus)
    {
        if (gameStatus.Phase != Phase.Placement) throw new InvalidOperationException();
        return new PlacementPhase(gameStatus);
    }

    public IChallengePhase BeginChallengePhase()
    {
        GameState.GoToNextPhase();
        return ChallengePhase.CreateFromState(GameState);
    }

    public IGameState PlaceCoaster(int player, bool isSkull)
    {
        if (player != GameState.NextPlayer) throw new OutOfTurnException($"Player {player} tried to play on Player {GameState.NextPlayer}'s turn.");
        var playerState = isSkull switch
        {
            true => GameState.Players.Single(p => p.PlayerId == player).PlaySkull(),
            false => GameState.Players.Single(p => p.PlayerId == player).PlayFlower()
        };
        GameState.GoToNextPlayer();
        return GameState;
    }
}
