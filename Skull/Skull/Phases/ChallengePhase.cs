using Skull.Exceptions;
using Skull.GamesState;

namespace Skull.Phases;

internal sealed class ChallengePhase : IChallengePhase
{
    public IGameState GameState { get; }
    private ChallengePhase(IGameState gameState)
    {
        GameState = gameState;
    }

    public static IChallengePhase CreateFromState(IGameState gameStatus)
    {
        if (gameStatus.Phase != Phase.Challenge) throw new InvalidOperationException();
        return new ChallengePhase(gameStatus);
    }

    public RevealPhase BeginRevealPhase()
    {
        return new RevealPhase(GameState);
    }

    public IGameState MakeBid(int player, int? bid)
    {
        if (player != GameState.NextPlayer) throw new OutOfTurnException($"Player {player} tried to play on Player {GameState.NextPlayer}'s turn.");
        if (bid != null && bid.Value <= GameState.Bids.Max(x => x.CardsToReveal)) throw new InsufficientBidException($"Player {player} bid {bid} which is not more than the highest bid so far.");
        GameState.Bids.Push(new Bid { PlayerId = player, CardsToReveal = bid });
        do
        {
            GameState.GoToNextPlayer();
        } while (GameState.Bids.Any(b => b.PlayerId == GameState.NextPlayer && b.CardsToReveal == null));
        return GameState;
    }
}
