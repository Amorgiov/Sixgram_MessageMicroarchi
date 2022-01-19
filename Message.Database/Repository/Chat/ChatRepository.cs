using Message.Database.Context;
using Message.Database.Models;
using Message.Database.Repository.Base;

namespace Message.Database.Repository.Chat
{
    public class ChatRepository : BaseRepository<ChatEntity>, IChatRepository
    {
        public ChatRepository(ApplicationContext context) : base(context) { }
    }
}