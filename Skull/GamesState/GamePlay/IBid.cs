namespace Skull.GamesState.GamePlay
{
    public interface IBid
    {
        int PlayerId { get; }
        int? CardsToReveal { get; }
    }
}
