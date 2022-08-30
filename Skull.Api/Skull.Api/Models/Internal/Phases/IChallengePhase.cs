using SkullApi.Models.Internal.GamesState;

namespace SkullApi.Models.Internal.Phases
{
    public interface IChallengePhase
    {
        RevealPhase BeginRevealPhase();
        IGameState MakeBid(int player, int? bid);
    }
}