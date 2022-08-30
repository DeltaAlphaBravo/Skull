using Skull.GamesState;

namespace Skull.Api.Models
{
    public sealed class GamePlayerView : IGamePlayerView
    {
        public int PlayerId { get; init; }

        public int PlayerWithOnus { get; init; }

        public IEnumerable<IOpponentState> OpponentStates { get; init; }

        public IReadOnlyHand Hand { get; init; }

        public Stack<bool> PlayedCoasters { get; private init; }

        public GamePlayerView(IGameState gameState, int playerId)
        {
            PlayerId = playerId;
            PlayerWithOnus = gameState.NextPlayer;
            Hand = gameState.Players.Where(h => h.PlayerId == playerId)
                                    .Select(h => new ReadOnlyHand { CardCount = h.Hand.CardCount, HasSkull = h.Hand.HasSkull, PlayerId = h.Hand.PlayerId })
                                    .Single();
            OpponentStates = gameState.Players.Where(p => p.PlayerId != playerId)
                                              .Select(p =>
                                              {
                                                  var opponentHand = new ReadOnlyHand
                                                  {
                                                      CardCount = p.Hand.CardCount,
                                                      HasSkull = p.Hand.HasSkull,
                                                      PlayerId = p.Hand.PlayerId
                                                  };
                                                  return new OpponentState(opponentHand)
                                                  {
                                                      StackCount = p.PlayedCoasters.Count(),
                                                      PlayerId = p.PlayerId
                                                  };
                                              })
                                              .ToList();
            PlayedCoasters = gameState.Players.Where(h => h.PlayerId == playerId)
                                              .Select(h => new Stack<bool>(h.PlayedCoasters.Select(c => c == Coaster.Skull)))
                                              .Single();
        }
    }
}
