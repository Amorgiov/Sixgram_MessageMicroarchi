using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Message.Core.Services.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private string UserName => Context?.User?.Identity?.Name ?? "Unknown"; 
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
        
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        [HubMethodName("SendAll")]
        public async Task SendMessage(string message) => await Clients.All.ReceiveMessage(UserName, message);

        [HubMethodName("SendToGroup")]
        public async Task SendMessageToGroup(string group, string message) =>
            await Clients.Group(group).ReceiveMessage(UserName, message);
        
        [HubMethodName("SendToCaller")]
        public async Task SendMessageToCaller(string message)
            => await Clients.Caller.ReceiveMessage(UserName, message);
        
        [HubMethodName("SendPrivate")]
        public async Task SendToUser(string user, string message)
            => await Clients.Client(user).ReceiveMessage(UserName, message);
    }
}