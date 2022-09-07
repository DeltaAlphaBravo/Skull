using Skull.GamesState;

namespace Skull.Phases
{
    internal interface IChallengePhase
    {
        RevealPhase BeginRevealPhase();
        IGameState MakeBid(int player, int? bid);
    }
}