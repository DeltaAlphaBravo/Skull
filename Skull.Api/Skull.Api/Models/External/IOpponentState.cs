namespace SkullApi.Models.External
{
    public interface IOpponentState
    {
        int PlayerId { get; }
        IOpponentHand Hand { get; }
        int StackCount { get; }
    }
}
