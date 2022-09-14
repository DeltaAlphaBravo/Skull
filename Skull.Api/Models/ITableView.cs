namespace Skull.Api.Models
{
    public interface ITableView
    {
        string Name { get; }
        IReadOnlyList<IPlayer> Players { get; }
    }
}