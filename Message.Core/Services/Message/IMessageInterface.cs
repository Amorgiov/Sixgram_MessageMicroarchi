using System.Collections.Generic;
using System.Threading.Tasks;
using Message.Core.Dto;
using Message.Database.Models;
using Message.Database.Models.User;
using UserEntity = Message.Database.Models.UserEntity;

namespace Message.Core.Services.Message
{
    public interface IMessageInterface
    {
        void CreateChat(List<int> userIds, string title);
        void DeleteChat(int userId);
        
        /// <param name="messageIds"></param>
        /// <param name="deleteForAll">delete for the recipient</param>
        /// <returns>1 for each message</returns>
        int DeleteMessage(List<int> messageIds, int deleteForAll);

        /// <returns>array of objects describing the messages</returns>
        IEnumerable<MessageEntity> GetMessageById(List<int> messageIds, int previewLength = 0);

        /// <returns>array of objects describing the chats</returns>
        IEnumerable<ChatEntity> GetConversations(int userId);
        IEnumerable<ChatEntity> GetConversationsById(int? chatId, List<int> chatIds);

        /// <returns>array of objects describing users and info user rights</returns>
        IEnumerable<ChatUsers> GetChatMembers(int chatId);
        
        /// <returns>messageId</returns>
        int Send(MessageDto messageDto);
    }
}