using Skull.GamesState;

namespace Skull.Api.Models
{
    public sealed class GamePlayerView : IGamePlayerView
    {
        public int PlayerId { get; init; }

        public int NextPlayer { get; init; }

        public IEnumerable<IOpponentState> OpponentStates { get; init; }

        public IReadOnlyHand Hand { get; init; }

        public Stack<bool> PlayedCoasters { get; private init; }

        public GamePlayerView(IGameState gameState, int playerId)
        {
            PlayerId = playerId;
            NextPlayer = gameState.NextPlayer;
            Hand = gameState.Players.Where(h => h.PlayerId == playerId)
                                    .Select(h => new ReadOnlyHand
                                    {
                                        CardCount = h.PlayerState.Hand.CardCount,
                                        HasSkull = h.PlayerState.Hand.HasSkull,
                                        PlayerId = h.PlayerState.Hand.PlayerId
                                    })
                                    .Single();
            OpponentStates = gameState.Players.Where(p => p.PlayerId != playerId)
                                              .Select(p =>
                                              {
                                                  var opponentHand = new ReadOnlyHand
                                                  {
                                                      CardCount = p.PlayerState.Hand.CardCount,
                                                      HasSkull = p.PlayerState.Hand.HasSkull,
                                                      PlayerId = p.PlayerState.Hand.PlayerId
                                                  };
                                                  return new OpponentState(opponentHand)
                                                  {
                                                      StackCount = p.PlayerState.PlayedCoasters.Count(),
                                                      PlayerId = p.PlayerId
                                                  };
                                              })
                                              .ToList();
            PlayedCoasters = gameState.Players.Where(h => h.PlayerId == playerId)
                                              .Select(h => new Stack<bool>(h.PlayerState.PlayedCoasters.Select(c => c == Coaster.Skull)))
                                              .Single();
        }
    }
}
