﻿using Skull.GamesState;

namespace Skull.Api.Models
{
    public sealed class GamePlayerView : IGamePlayerView
    {
        public int PlayerId { get; init; }

        public int NextPlayer { get; init; }

        public IEnumerable<IOpponentState> OpponentStates { get; init; }

        public IReadOnlyHand Hand { get; init; }

        public Stack<bool> PlayedCoasters { get; private init; }
        public IEnumerable<bool> Reveals { get; private init; }

        public string Phase { get; init; }

        public IEnumerable<IPlayerBid> Bids { get; init; }

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
                                                           PlayerId = p.PlayerId,
                                                           Reveals = gameState.Reveals.Where(r => r.PlayerId == p.PlayerId).Select(r => r.IsSkull)
                                                       };
                                                   })
                                                   .ToList();
            PlayedCoasters = gameState.PlayerStates.Where(h => h.PlayerId == playerId)
                                              .Select(h => new Stack<bool>(h.PlayedCoasters.Select(c => c == Coaster.Skull)))
                                              .Single();
            Reveals = gameState.Reveals.Where(r => r.PlayerId == playerId).Select(r => r.IsSkull);
            Phase = gameState.Phase.ToString();
            Bids = gameState.Phase != GamesState.Phase.Placement 
                   ? gameState.Bids.Select(b => new PlayerBid() { Bid = b.CardsToReveal, PlayerId = b.PlayerId })
                   : Array.Empty<IPlayerBid>();
        }
    }
}
