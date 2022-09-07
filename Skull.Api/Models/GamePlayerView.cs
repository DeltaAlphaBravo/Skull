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
            Hand = gameState.PlayerStates.Where(h => h.PlayerId == playerId)
                                         .Select(h => new ReadOnlyHand
                                         {
                                             CardCount = h.Hand.CardCount,
                                             HasSkull = h.Hand.HasSkull,
                                             PlayerId = h.PlayerId
                                         })
                                         .Single();
            OpponentStates = gameState.PlayerStates.Where(p => p.PlayerId != playerId)
                                                   .Select(p =>
                                                   {
                                                       var opponentHand = new ReadOnlyHand
                                                       {
                                                           CardCount = p.Hand.CardCount,
                                                           HasSkull = p.Hand.HasSkull,
                                                           PlayerId = p.PlayerId
                                                       };
                                                       return new OpponentState(p.PlayerId,
                                                                                opponentHand)
                                                       {
                                                           
                                                           StackCount = p.PlayedCoasters.Count(),
                                                           PlayerId = p.PlayerId
                                                       };
                                                   })
                                                   .ToList();
            PlayedCoasters = gameState.PlayerStates.Where(h => h.PlayerId == playerId)
                                              .Select(h => new Stack<bool>(h.PlayedCoasters.Select(c => c == Coaster.Skull)))
                                              .Single();
        }
    }
}
