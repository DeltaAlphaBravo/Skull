using SkullApi.Models.Internal.GamesState;

namespace SkullApi.Models.Internal.Phases
{
    public interface IPlacementPhase
    {
        IChallengePhase BeginChallengePhase();
        IGameState PlaceCoaster(int player, bool isSkull);
    }
}