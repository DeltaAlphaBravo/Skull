using Skull.Skull.GamesState.GamePlay;

namespace Skull.Skull.GamesState.Player
{
    public interface IPlayerState
    {
        IHand Hand { get; }
        Stack<Coaster> PlayedCoasters { get; }
    }
}
