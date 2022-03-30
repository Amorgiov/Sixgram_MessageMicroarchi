using System;
using System.Threading.Tasks;
using AutoMapper;
using Message.Common.Enums;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Message;
using Message.Core.Services.Token;
using Message.Database.Models;
using Message.Database.Repository.Chat;
using Message.Database.Repository.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace Message.Core.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IMessageRepository _messageRepository;

        public MessageService
        (
            IMapper mapper,
            IMessageRepository messageRepository, 
            ITokenService tokenService)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _tokenService = tokenService;
        }

        public async Task<ResultContainer<MessageDto>> AddMessage(MessageDto data, Guid chatId)
        {
            var result = new ResultContainer<MessageDto>();

            if (data == null)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var message = new MessageEntity()
            {
                SenderId = _tokenService.GetCurrentUserId(),
                Text = data.Text,
                ChatId = chatId
            };
            
            result = _mapper.Map<ResultContainer<MessageDto>>(await _messageRepository.Create(message));
            return result;
        }

        public async Task<ResultContainer> SendFile()
        {
            
        } 
        
        
        public async Task<ResultContainer<MessageDto>> GetMessageById(Guid mesId)
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