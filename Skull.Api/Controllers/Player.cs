using Skull.GamesState.Player;

namespace Skull.Api.Controllers;

public class Player : IPlayer
{
    public string Name { get; private init; }

    public int PlayerId { get; private init; }

    public Player(IPlayerIdentity player)
    {
        Name = player.Name;
        PlayerId = player.PlayerId;
    }
}
