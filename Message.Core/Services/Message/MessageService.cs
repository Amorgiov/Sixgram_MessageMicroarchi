using System;
using System.Threading.Tasks;
using AutoMapper;
using Message.Common.Enums;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Message;
using Message.Core.Services.File;
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
        private IFileStorageService _fileStorage;

        public MessageService
        (
            IMapper mapper,
            IMessageRepository messageRepository, 
            ITokenService tokenService,
            IFileStorageService fileStorageService
        )
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _tokenService = tokenService;
            _fileStorage = fileStorageService;
        }

        public async Task<ResultContainer> AddMessage(CreateMessageDto data, Guid chatId)
        {
            var result = new ResultContainer<CreateMessageDto>();
            Guid? fileId = null;
            
            var messageId = new Guid();

            if (data.File != null)
            { 
                fileId = await _fileStorage.CreateFile(data.File, messageId);
                
                if (fileId == null)
                {
                    result.ResponseStatusCode = ResponseStatusCode.BadRequest;
                    return result;
                }
            }

            var message = new MessageEntity()
            {
                Id = messageId,
                SenderId = _tokenService.GetCurrentUserId(),
                Text = data.Text,
                ChatId = chatId,
                FileId = fileId
            };

            await _messageRepository.Create(message);
            
            result.ResponseStatusCode = ResponseStatusCode.NoContent;
            
            return result;
        }

        public async Task<ResultContainer<CreateMessageDto>> GetMessageById(Guid mesId)
        {
            var result = new ResultContainer<CreateMessageDto>();
            var message = await _messageRepository.GetById(mesId);

            if (message == null)
            {
                result.ResponseStatusCode = ResponseStatusCode.NotFound;
                return result;
            }

            var messageDto = new CreateMessageDto()
            {
                Text = message.Text,
                SenderId = (Guid) message.SenderId,
                File = null
            };

            result = _mapper.Map<ResultContainer<CreateMessageDto>>(messageDto);
            result.ResponseStatusCode = ResponseStatusCode.Ok;
            
            return result;
        }
        
        public async Task<ResultContainer> Delete(Guid messageId)
        {
            var result = new ResultContainer();

            var message = await _messageRepository.GetById(messageId);

            if (message == null)
            {
                result.ResponseStatusCode = ResponseStatusCode.NotFound;
                return result;
            }

            if (message.SenderId != _tokenService.GetCurrentUserId())
            {
                result.ResponseStatusCode = ResponseStatusCode.BadRequest;
                return result;
            }
        
            await _messageRepository.Delete(message);

            if (message.FileId != null)
                await _fileStorage.DeleteFile((Guid)message.FileId);    

            result.ResponseStatusCode = ResponseStatusCode.NoContent;

            return result;
        }
    }
}