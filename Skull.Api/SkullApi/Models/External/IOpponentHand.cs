namespace SkullApi.Models.External
{
    public interface IOpponentHand
    {
        int CardCount { get; }
        int PlayerId { get; }
    }
}