using Message.Database.Models;
using Message.Database.Repository.Base;

namespace Message.Database.Repository.Message
{
    public interface IMessageRepository : IBaseRepository<MessageEntity>
    {
        
    }
}