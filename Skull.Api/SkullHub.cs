using Microsoft.AspNetCore.SignalR;

namespace Skull.Api
{
    public class SkullHub : Hub, ISkullHub
    {
        private readonly IHubContext<SkullHub> _hubContext;

        public SkullHub(IHubContext<SkullHub> hubContext) => _hubContext = hubContext;

        public async Task SubscribeToNotificationsAsync(string table)
        {
            await _hubContext.Groups.AddToGroupAsync(Context.ConnectionId, table);
        }

        public async Task NotifyNewPlayer(string table, string playerName, int playerId)
        {
            await _hubContext.Clients.Group(table).SendAsync("ReceiveNewPlayer", playerName, playerId);
        }

        public async Task NotifyGameStarted(string table)
        {
            await _hubContext.Clients.Group(table).SendAsync("ReceiveGameStart");
        }

        public async Task NotifyNewPlacement(string table, int playerId)
        {
            await _hubContext.Clients.Group(table).SendAsync("ReceiveNewPlacement", playerId);
        }

        public async Task NotifyNewBid(string table, int playerId, int? bid)
        {
            await _hubContext.Clients.Group(table).SendAsync("ReceiveNewBid", playerId, bid);
        }
    }
}
