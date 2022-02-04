using System;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto;

namespace Message.Core.Services.Message
{
    public interface IMessageService
    {
        Task<ResultContainer<MessageDto>> SendMessage(MessageDto messageDto);
        Task<ResultContainer<MessageDto>> GetMessageById(Guid mesId);
        //Task<ResultContainer<MessageDto>> DeleteMessage(int mesId);
    }
}