using Microsoft.AspNetCore.SignalR;

namespace Skull.Api
{
    public class SkullHub : Hub, ISkullHub
    {
        public async Task AddToGroupAsync(string game)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, game);
        }

        public async Task SendMessageAsync(string game, string message)
        {
            await Clients.Group(game).SendAsync("ReceiveMessage", message);
        }
    }
}
