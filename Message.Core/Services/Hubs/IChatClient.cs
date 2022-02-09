using System.Threading.Tasks;

namespace Message.Core.Services.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
}