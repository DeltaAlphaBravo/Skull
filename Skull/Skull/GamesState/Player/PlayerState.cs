﻿namespace Skull.GamesState
{
    internal class PlayerState : IPlayerState
    {
        public IHand Hand { get; init; }

        public Stack<Coaster> PlayedCoasters { get; init; }

        public PlayerState(int playerId)
        {
            PlayedCoasters = new Stack<Coaster>();
            Hand = new Hand { PlayerId = playerId, CardCount = 4, HasSkull = true };
        }
    }
}