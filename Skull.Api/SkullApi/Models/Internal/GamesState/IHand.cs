namespace SkullApi.Models.Internal.GamesState
{
    public interface IHand
    {
        int PlayerId { get; init; }
        int CardCount { get; set; }
        bool HasSkull { get; set; }
    }
}