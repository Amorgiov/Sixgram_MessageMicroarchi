using System;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Message;

namespace Message.Core.Services.Message
{
    public interface IMessageService
    {
        Task<ResultContainer<MessageDto>> AddMessage(MessageDto messageDto, Guid chatId);
        Task<ResultContainer<MessageDto>> GetMessageById(Guid mesId);
        //Task<ResultContainer<MessageDto>> DeleteMessage(int mesId);
    }
}