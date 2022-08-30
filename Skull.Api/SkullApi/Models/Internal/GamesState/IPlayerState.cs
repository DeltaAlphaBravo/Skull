namespace SkullApi.Models.Internal.GamesState
{
    public interface IPlayerState
    {
        int PlayerId { get; }
        IHand Hand { get; }
        Stack<Coaster> PlayedCoasters { get; }
    }
}
