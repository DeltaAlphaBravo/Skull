using Microsoft.AspNetCore.SignalR;

namespace Skull.Api
{
    public class SkullHub : Hub, ISkullHub
    {
        private readonly IHubContext<SkullHub> _hubContext;
        private readonly ILogger<SkullHub> _logger;
        private readonly Dictionary<TablePlayerPair, string> _subscriptions = new();

        public SkullHub(IHubContext<SkullHub> hubContext, ILogger<SkullHub> logger)
        {
            _hubContext = hubContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SubscribeToNotificationsAsync(string table, int playerId)
        {
            _logger.LogInformation("Subscription to {table} by player with number {playerId}", table, playerId);
            var key = new TablePlayerPair { Table = table, Player = playerId };
            if(_subscriptions.ContainsKey(key))
            {
                await _hubContext.Groups.RemoveFromGroupAsync(_subscriptions[key], table);
                _subscriptions.Remove(key);
            }
            await _hubContext.Groups.AddToGroupAsync(Context.ConnectionId, table);
            _subscriptions.Add(key, table);
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

        public async Task NotifyNewReveal(string table, int playerId, bool isSkull)
        {
            await _hubContext.Clients.Group(table).SendAsync("ReceiveNewReveal", playerId, isSkull);
        }

        private record TablePlayerPair
        {
            public string Table { init; get; } = string.Empty;
            public int Player { init; get; }
        }
    }
}
