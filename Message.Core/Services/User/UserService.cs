using System.Collections.Generic;
using System.Net.Http;
using Message.Database.Models.User;

namespace Message.Core.Services.User
{
    public class UserService : IUserService
    {
        private HttpClient _client;
        public UserService(HttpClient client)
        {
            _client = client;
        }

        public int AddChatUser(int chatId, int userId)
        {
            throw new System.NotImplementedException();
        }

        public int RemoveChatUser(int chatId, int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ChatUsers> GetChatMembers(int chatId)
        {
            throw new System.NotImplementedException();
        }
    }
}