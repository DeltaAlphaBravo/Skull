using Skull.GamesState.GamePlay;

namespace Skull.GamesState.Player
{
    public interface IPlayerState
    {
        int PlayerId { get; }
        IHand Hand { get; }
        Stack<Coaster> PlayedCoasters { get; }
        int? CardsToReveal { get; }
        IPlayerState PlaySkull();
        IPlayerState PlayFlower();
    }
}
