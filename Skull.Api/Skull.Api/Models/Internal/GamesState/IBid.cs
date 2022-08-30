namespace SkullApi.Models.Internal.GamesState
{
    public interface IBid
    {
        int PlayerId { get; }
        int? CardsToReveal { get; }
    }
}
