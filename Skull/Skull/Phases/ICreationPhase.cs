using Skull.GamesState;

namespace Skull.Phases
{
    internal interface ICreationPhase
    {
        IGameState GameState { get; }
        int JoinPlayer(string name);
        IGameState PlaceFirstCoaster(int player, bool isSkull);
    }
}