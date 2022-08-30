namespace SkullApi.Models.External
{
    public interface IGamePlayerView
    {
        int PlayerId { get; }
        int PlayerWithOnus { get; }
        IEnumerable<IOpponentState> OpponentStates { get; }
        IReadOnlyHand Hand { get; }
        Stack<bool> PlayedCoasters { get; }
    }
}
