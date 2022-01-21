using Message.Database.Context;
using Message.Database.Models;
using Message.Database.Repository.Base;

namespace Message.Database.Repository.Message
{
    public class MessageRepository : BaseRepository<MessageEntity>, IMessageRepository
    {
        public MessageRepository(ApplicationContext context) : base(context) { }
    }
}