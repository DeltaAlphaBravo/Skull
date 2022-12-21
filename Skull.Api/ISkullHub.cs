
namespace Skull.Api
{
    public interface ISkullHub
    {
        Task SubscribeToNotificationsAsync(string table, int playerId);
        Task NotifyNewPlayer(string table, string playerName, int playerId);
        Task NotifyGameStarted(string table);
        Task NotifyNewPlacement(string table, int playerId);
        Task NotifyNewBid(string table, int playerId, int? bid);
    }
}