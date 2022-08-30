using SkullApi.Models.External;

namespace SkullApi.Models.Internal.GamesState
{
    public sealed class Hand : IHand, IReadOnlyHand, IOpponentHand
    {
        public int PlayerId { get; init; }

        public bool HasSkull { get; set; }

        public int CardCount { get; set; }
    }
}
