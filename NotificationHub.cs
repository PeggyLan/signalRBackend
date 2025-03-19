using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs
{
    public class NotificationHub : Hub
    {
        // 加入群組
        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Caller.SendAsync("ReceiveMessage", $"{Context.ConnectionId} 加入了群組 {group}");
        }

        // 離開群組
        public async Task LeaveGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            await Clients.Caller.SendAsync("ReceiveMessage", $"{Context.ConnectionId} 離開了群組 {group}");
        }

        // 私人訊息
        public async Task SendToPrivate(string sendPrivateMessageTarget, string user, string message)
        {
            await Clients.Client(sendPrivateMessageTarget).SendAsync("ReceivePrivateMessage", user, message);
        }

        // 群組訊息
        public async Task SendToGroup(string group, string user, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveGroupMessage", user, group, message);
        }

        // 廣播訊息
        public async Task Broadcast(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        // 客戶端連線時觸發
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
