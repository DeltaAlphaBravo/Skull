using Skull.GamesState;

namespace Skull
{
    public interface ISkullGame
    {
        Task<IGameState> StartGameAsync(ITable table);
        Task<IGameState?> GetGameStateAsync(string tableName);
        Task<IGameState> MakeBidAsync(string tableName, int player, int? cardsToReveal);
        Task<IGameState> PlaceCoasterAsync(string tableName, int player, bool isSkull);
    }
}