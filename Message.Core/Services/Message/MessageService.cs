using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Message.Core.Dto;
using Message.Database.Models;
using Message.Database.Models.User;

namespace Message.Core.Services.Message
{
    public class MessageService : IMessageInterface
    {
        private HttpClient _client;
        
        public MessageService(HttpClient client)
        {
            _client = client;
        }
        
        public void CreateChat(List<int> userIds, string title)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteChat(int userId)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteMessage(List<int> messageIds, int deleteForAll)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MessageEntity> GetMessageById(List<int> messageIds, int previewLength = 0)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ChatEntity> GetConversations(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ChatEntity> GetConversationsById(int? chatId, List<int> chatIds)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ChatUsers> GetChatMembers(int chatId)
        {
            throw new System.NotImplementedException();
        }

        public int Send(MessageDto messageDto)
        {
            throw new System.NotImplementedException();
        }
    }
}