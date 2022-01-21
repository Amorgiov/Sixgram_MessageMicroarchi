using System.Threading.Tasks;
using AutoMapper;
using Message.Common.Enums;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Database.Models;
using Message.Database.Repository.Chat;
using Message.Database.Repository.Message;
using Microsoft.EntityFrameworkCore.Storage;

namespace Message.Core.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMapper mapper, IMessageRepository messageRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public async Task<ResultContainer<MessageDto>> SendMessage(MessageDto data)
        {
            var result = new ResultContainer<MessageDto>();
            if (data   == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            var message = _mapper.Map<MessageEntity>(data);
            message.Text = data.Text;
            message.ChatId = data.ChatId;
            message.SenderId = data.SenderId;
            
            result = _mapper.Map<ResultContainer<MessageDto>>(await _messageRepository.Create(message));
            return result;
        }

        public async Task<ResultContainer<MessageDto>> GetMessageById(int mesId)
        {
            var result = new ResultContainer<MessageDto>();
            var message = await _messageRepository.GetById(mesId);

            if (message == null)
            {
                result.ErrorType = ErrorType.NotFound;
            }

            result = _mapper.Map<ResultContainer<MessageDto>>(message);
            return result;
        }
    }
}