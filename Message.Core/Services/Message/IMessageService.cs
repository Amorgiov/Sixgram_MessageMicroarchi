using System;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Message;

namespace Message.Core.Services.Message
{
    public interface IMessageService
    {
        Task<ResultContainer> AddMessage(CreateMessageDto createMessageDto, Guid chatId);
        Task<ResultContainer<CreateMessageDto>> GetMessageById(Guid mesId);
        Task<ResultContainer> Delete(Guid postId);
    }
}