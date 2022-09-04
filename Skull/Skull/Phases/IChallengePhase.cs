using Skull.Skull.GamesState;

namespace Skull.Skull.Phases
{
    internal interface IChallengePhase
    {
        RevealPhase BeginRevealPhase();
        IGameState MakeBid(int player, int? bid);
    }
}