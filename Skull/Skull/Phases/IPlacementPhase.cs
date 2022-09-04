using Skull.Skull.GamesState;

namespace Skull.Skull.Phases
{
    internal interface IPlacementPhase
    {
        IChallengePhase BeginChallengePhase();
        IGameState PlaceCoaster(int player, bool isSkull);
    }
}