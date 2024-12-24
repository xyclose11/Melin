using Microsoft.AspNetCore.SignalR;

namespace Melin.Server.Hubs;

public class DocumentHub : Hub
{
    public async Task NewMessage(string user, string message)
    {
        await Clients.All.SendAsync("messageReceived", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        var userName = Context.User?.Identity?.Name ?? "Guest";

        await Clients.All.SendAsync("UserConnected", userName);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userName = Context.User?.Identity?.Name ?? "Guest";
        await Clients.All.SendAsync("UserDisconnected", userName);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task UpdateContent(string documentId, string content)
    {
        await Clients.Others.SendAsync("ReceiveContentUpdate", documentId, content);
    }

    public async Task UpdateCursor(string user, int position)
    {
        await Clients.Others.SendAsync("ReceiveCursorUpdate", user, position);
    }
}