using Skull.GamesState.GamePlay;

namespace Skull.GamesState.Player
{
    public interface IPlayerState
    {
        int PlayerId { get; }
        IHand Hand { get; }
        Stack<Coaster> PlayedCoasters { get; }
        int? CardsToReveal { get; }
        bool HasWonAGame { get; }
        IPlayerState PlaySkull();
        IPlayerState PlayFlower();
        IPlayerState RecordVictory();
    }
}
