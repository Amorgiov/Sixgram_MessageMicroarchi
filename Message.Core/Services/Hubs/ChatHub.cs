using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Message.Core.Services.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendAsync()
        {
            
        }
    }
}