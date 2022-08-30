namespace Skull.Api.Models
{
    public interface IReadOnlyHand
    {
        int CardCount { get; }
        int PlayerId { get; }
        bool HasSkull { get; }
    }
}