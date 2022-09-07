namespace Skull.Api.Controllers
{
    public interface ITableView
    {
        string Name { get; }
        IReadOnlyList<IPlayer> Players { get; }
    }
}