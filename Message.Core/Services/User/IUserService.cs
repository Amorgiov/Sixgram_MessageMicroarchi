using System.Collections.Generic;
using Message.Database.Models.User;

namespace Message.Core.Services.User
{
    public interface IUserService
    {
        /// <returns>1 if Ok</returns>
        int AddChatUser(int chatId, int userId);
        int RemoveChatUser(int chatId, int userId);
        
        /// <returns>array of objects describing users and info user rights</returns>
        IEnumerable<ChatUsers> GetChatMembers(int chatId);
    }
}