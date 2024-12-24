using Microsoft.AspNetCore.SignalR;

namespace Melin.Server.Hubs;

public class DocumentHub : Hub
{
    public async Task NewMessage(string user, string message)
    {
        await Clients.All.SendAsync("messageReceived", user, message);
    }

}