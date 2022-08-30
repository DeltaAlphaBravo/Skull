using SkullApi.Models.Internal.Exceptions;
using SkullApi.Models.Internal.GamesState;

namespace SkullApi.Models.Internal.Phases
{
    public sealed class ChallengePhase : IChallengePhase
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
            if (player != GameState.PlayerWithOnus) throw new OutOfTurnException($"Player {player} tried to play on Player {GameState.PlayerWithOnus}'s turn.");
            if (bid != null && bid.Value <= GameState.Bids.Max(x => x.CardsToReveal)) throw new InsufficientBidException($"Player {player} bid {bid} which is not more than the highest bid so far.");
            GameState.Bids.Push(new Bid { PlayerId = player, CardsToReveal = bid });
            do {
                GameState.NextPlayer();
            } while (GameState.Bids.Any(b => b.PlayerId == GameState.PlayerWithOnus && b.CardsToReveal == null));
            return GameState;
        }
    }
}
