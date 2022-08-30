namespace SkullApi.Models.Internal.GamesState
{
    public class Bid : IBid
    {
        public int PlayerId { get; init; }
        public int? CardsToReveal { get; init; }
    }
}
