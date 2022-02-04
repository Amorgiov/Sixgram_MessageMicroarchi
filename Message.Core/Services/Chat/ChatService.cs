using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Message.Common.Enums;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Chat;
using Message.Core.Dto.Update;
using Message.Core.Services.Token;
using Message.Database.Models;
using Message.Database.Repository.Chat;

namespace Message.Core.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        private readonly ITokenService _tokenService;
        public ChatService(IMapper mapper, IChatRepository chatRepository, ITokenService tokenService)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            _tokenService = tokenService;
        }

        public async Task<ResultContainer<ChatDto>> CreateChat(ChatEntity model)
        {
            var currentUser = _tokenService.GetCurrentUserId();
            model.Admin = currentUser;
            return _mapper.Map<ResultContainer<ChatDto>>(await _chatRepository.Create(model));
        }

        public async Task<ResultContainer<ChatUpdateResponseDto>> DeleteChat(Guid id)
        {
            var result = new ResultContainer<ChatUpdateResponseDto>();
            var chat = await _chatRepository.Delete(id);
            if (chat == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<ChatUpdateResponseDto>>(chat);
            return result;
        }
        
        public async Task<ResultContainer<ChatUpdateResponseDto>> EditChat(ChatUpdateRequestDto data, Guid id)
        {
            var result = new ResultContainer<ChatUpdateResponseDto>();
            
            //var user = _tokenService.GetCurrentUserId();
            var chat = _chatRepository.GetById(id);
            
            if (chat == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            chat.Result.Title = data.NewTitle;
            chat.Result.Members = data.NewMembers;

            result = _mapper.Map<ResultContainer<ChatUpdateResponseDto>>(await _chatRepository.Update(chat.Result));
            return result;
        }

        public async Task<ResultContainer<ChatDto>> GetChatById(Guid id)
        {
            var result = new ResultContainer<ChatDto>();
            var chat = await _chatRepository.GetById(id);
            if (chat == null)
            {
                result.ErrorType = ErrorType.NotFound;
            }

            result = _mapper.Map<ResultContainer<ChatDto>>(chat);
            return result;
        }
    }
}