using Microsoft.AspNetCore.SignalR;

namespace Supermercado.API.Config.Hubs
{
    public class SensorHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
