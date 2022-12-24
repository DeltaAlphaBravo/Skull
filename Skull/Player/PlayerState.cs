using Skull.GamesState;
using Skull.GamesState.GamePlay;

namespace Skull.GamesState.Player
{
    internal class PlayerState : IPlayerState
    {
        public IHand Hand { get; init; }

        public Stack<Coaster> PlayedCoasters { get; init; }

        public int PlayerId { get; init; }

        public int? CardsToReveal { get; private set; }

        public bool HasWonAGame { get; private set; }

        public PlayerState(int playerId)
        {
            PlayerId = playerId;
            PlayedCoasters = new Stack<Coaster>();
            Hand = new Hand { CardCount = 4, HasSkull = true };
        }

        public IPlayerState PlaySkull()
        {
            if (!Hand.HasSkull) throw new InvalidOperationException($"Player {PlayerId} tried to play a skull, but has no skull");
            Hand.HasSkull = false;

            return PlayCard(Coaster.Skull);
        }

        public IPlayerState PlayFlower()
        {
            if (Hand.HasSkull && Hand.CardCount == 1)
                throw new InvalidOperationException($"Player {PlayerId} tried to play a flower, but has no flower");
            if (Hand.CardCount < 1)
                throw new InvalidOperationException($"Player {PlayerId} tried to play a flower, but has no cards");
            return PlayCard(Coaster.Flower);
        }

        public IPlayerState RecordVictory()
        {
            HasWonAGame = true;
            return this;
        }

        private IPlayerState PlayCard(Coaster coaster)
        {
            PlayedCoasters.Push(coaster);
            Hand.CardCount--;
            return this;
        }
    }
}
