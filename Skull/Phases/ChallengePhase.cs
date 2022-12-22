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
        FindNextPlayer();
        return GameState;

        void FindNextPlayer()
        {
            if (CurrentPlayerHasToReveal()) return;
            do
            {
                GameState.GoToNextPlayer();
            } while (CurrentPlayerPassedOnBidding() && !CurrentPlayerHasToReveal());
        }

        bool CurrentPlayerPassedOnBidding()
        {
            return GameState.Bids.Any(b => b.PlayerId == GameState.NextPlayer && b.CardsToReveal == null);
        }

        bool CurrentPlayerHasToReveal()
        {
            var max = GameState.Bids.Max(b => b.CardsToReveal);
            var maxBid = GameState.Bids.Single(b => b.CardsToReveal == max);

            int totalPlayedCoasters = GameState.PlayerStates.SelectMany(p => p.PlayedCoasters).Count();
            var isMaxPossibleBidPlaced = max > 0 && max == totalPlayedCoasters;
            if(isMaxPossibleBidPlaced) return true;

            return (GameState.Bids.Count(b => b.CardsToReveal == null) == GameState.PlayerStates.Count - 1)
                && (GameState.Bids.SingleOrDefault(b => b.CardsToReveal == max)?.PlayerId == GameState.NextPlayer);
        }
    }
}
