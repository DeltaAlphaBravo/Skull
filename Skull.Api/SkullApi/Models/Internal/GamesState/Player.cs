namespace SkullApi.Models.Internal.GamesState
{
    public class Player : IPlayer
    {
        public int PlayerId { get; private init; }
        public IHand Hand { get; private init; }

        public Player(int playerId)
        {
            Hand = new Hand { PlayerId = PlayerId, CardCount = 4, HasSkull = true };
            PlayedCoasters = new Stack<Coaster>();
            PlayerId = playerId;
        }

        public Stack<Coaster> PlayedCoasters { get; set; }

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

        private IPlayerState PlayCard(Coaster coaster)
        {
            PlayedCoasters.Push(coaster);
            Hand.CardCount--;
            return this;
        }
    }
}
