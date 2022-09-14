using Skull.Api.Controllers;

namespace Skull.Api.Models;

internal class TableView : ITableView
{
    public string Name { get; private init; }

    public IReadOnlyList<IPlayer> Players { get; private init; }

    public TableView(ITable table)
    {
        Name = table.Name;
        Players = table.Players.Select(p => new Player(p)).ToList();
    }
}
