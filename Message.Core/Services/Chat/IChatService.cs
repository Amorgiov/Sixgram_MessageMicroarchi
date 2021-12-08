using System.Collections.Generic;
using System.Threading.Tasks;
using Message.Core.Dto;
using Message.Database.Models;

namespace Message.Core.Services.Chat
{
    public interface IChatService
    {
        void CreateChat(List<int> userIds, string title);
        void DeleteChat(int userId);
        
        /// <returns>array of objects describing the chats</returns>
        IEnumerable<ChatEntity> GetConversations(int userId);
        IEnumerable<ChatEntity> GetConversationsById(int? chatId, List<int> chatIds);
        
        /// <param name="messageIds"></param>
        /// <param name="deleteForAll">delete for the recipient</param>
        /// <returns>1 for each message</returns>
        Task<int> DeleteMessage(List<int> messageIds, int deleteForAll);

        /// <returns>array of objects describing the messages</returns>
        IEnumerable<MessageEntity> GetMessageById(List<int> messageIds, int previewLength = 0);

        /// <returns>messageId</returns>
        /*Task SendMsg(MessageDto messageDto);
        Task GetMsg(MessageDto messageDto);*/
    }
}