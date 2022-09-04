using Skull.Skull.GamesState;

namespace Skull.Skull
{
    public interface ISkullGame
    {
        Task<IGameState> CreateGameAsync();
        Task<IGameState> StartGame(string game);
        Task<IGameState?> GetGameStateAsync(string name);
        Task<IGameState> MakeBidAsync(string name, int player, int? cardsToReveal);
        Task<IGameState> PlaceCoasterAsync(string name, int player, bool isSkull);
        Task<IGameState> JoinPlayer(string name, string playerName, bool isSkull);
    }
}