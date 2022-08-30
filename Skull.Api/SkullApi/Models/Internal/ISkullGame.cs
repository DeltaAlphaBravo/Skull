
using SkullApi.Models.Internal.GamesState;

namespace SkullApi.Models.Internal
{
    public interface ISkullGame
    {
        Task<IGameState> CreateGameAsync(int playerCount);
        Task<IGameState?> GetGameStateAsync(string name);
        Task<IGameState?> MakeBidAsync(string name, int player, int? cardsToReveal);
        Task<IGameState?> PlaceCoasterAsync(string name, int player, bool isSkull);
    }
}