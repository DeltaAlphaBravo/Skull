using Skull.GamesState;

namespace Skull.Phases
{
    internal interface IPlacementPhase
    {
        IChallengePhase BeginChallengePhase();
        IGameState PlaceCoaster(int player, bool isSkull);
    }
}