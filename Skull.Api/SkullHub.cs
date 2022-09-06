using Microsoft.AspNetCore.SignalR;

namespace Skull.Api
{
    public class SkullHub : Hub, ISkullHub
    {
        private IHubContext<SkullHub> _hubContext;

        public SkullHub(IHubContext<SkullHub> hubContext) => _hubContext = hubContext;

        public async Task AddToGroupAsync(string game)
        {
            await _hubContext.Groups.AddToGroupAsync(Context.ConnectionId, game);
        }

        public async Task SendMessageAsync(string game, string message)
        {
            await _hubContext.Clients.Group(game).SendAsync("ReceiveMessage", message);
        }
    }
}
