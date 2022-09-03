namespace Skull.Api.Models
{
    public sealed class ReadOnlyHand : IReadOnlyHand, IOpponentHand
    {
        public int PlayerId { get; init; }

        public bool HasSkull { get; set; }

        public int CardCount { get; set; }
    }
}
