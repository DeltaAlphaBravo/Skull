namespace Skull.GamesState.GamePlay
{
    public interface IHand
    {
        int CardCount { get; set; }
        bool HasSkull { get; set; }
    }
}