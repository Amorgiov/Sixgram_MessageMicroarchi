using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Message.Core.Dto;
using Message.Database.Models;

namespace Message.Core.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _client;
        public ChatService(HttpClient httpClient)
        {
            _client = httpClient;
        }
        
        public void CreateChat(List<int> userIds, string title)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteChat(int userId)
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
        
        public Task<int> DeleteMessage(List<int> messageIds, int deleteForAll)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MessageEntity> GetMessageById(List<int> messageIds, int previewLength = 0)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendMsg(MessageDto messageDto)
        {
            throw new System.NotImplementedException();
        }

        public async Task GetMsg(MessageDto messageDto)
        {
            throw new System.NotImplementedException();
        }
    }
}