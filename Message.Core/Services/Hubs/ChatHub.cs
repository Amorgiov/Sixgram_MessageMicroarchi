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
            await base.OnDisconnectedAsync(exception);
        }
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceiveMessage(UserName, $" has joined the group {groupName}.");
        }
        
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceiveMessage(UserName, $" has left the group {groupName}.");
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