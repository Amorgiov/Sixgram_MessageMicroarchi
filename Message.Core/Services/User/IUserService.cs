using System.Collections.Generic;

namespace Message.Core.Services.User
{
    public interface IUserService
    {
        /// <returns>1 if Ok</returns>
        int AddChatUser(int chatId, int userId);
        int RemoveChatUser(int chatId, int userId);
        
        //IEnumerable<ChatUsers> GetChatMembers(int chatId);
    }
}