namespace Skull.GamesState
{
    public interface IBid
    {
        int PlayerId { get; }
        int? CardsToReveal { get; }
    }
}
