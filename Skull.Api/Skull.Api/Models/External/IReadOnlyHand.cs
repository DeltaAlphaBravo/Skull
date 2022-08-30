namespace SkullApi.Models.External
{
    public interface IReadOnlyHand
    {
        int CardCount { get; }
        int PlayerId { get; }
        bool HasSkull { get; }
    }
}