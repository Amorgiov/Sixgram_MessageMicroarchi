using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Message.Core.Services.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task Send(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }
        
        public async Task SendToCaller(string user, string message)
        {
            await Clients.Caller.ReceiveMessage(user, message);
        }
        
        public async Task SendToGroup(string user, string message)
        {
            await Clients.Group("SignalR Users").ReceiveMessage(user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public Task ThrowExeption()
        {
            throw new HubException("wow wow wow");
        }
    }
}