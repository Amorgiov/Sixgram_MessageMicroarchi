using System.Net.Http;

namespace Message.Core.Services.User
{
    public class UserService : IUserInterface
    {
        private HttpClient _client;
        public UserService(HttpClient client)
        {
            _client = client;
        }
        
        public void Connect()
        {
            throw new System.NotImplementedException();
        }

        public void Disconnect()
        {
            throw new System.NotImplementedException();
        }
    }
}