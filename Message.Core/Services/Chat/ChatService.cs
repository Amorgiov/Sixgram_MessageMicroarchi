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
using Message.Database.Repository.Member;

namespace Message.Core.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ITokenService _tokenService;


        public ChatService(IMapper mapper, IChatRepository chatRepository, ITokenService tokenService,
            IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            _tokenService = tokenService;
            _memberRepository = memberRepository;
        }

        public async Task<ResultContainer<ChatResponseDto>> CreateChat(ChatRequestDto model)
        {
            var result = new ResultContainer<ChatResponseDto>();

            var currentUser = _tokenService.GetCurrentUserId();

            if (currentUser == null)
            {
                result.ResponseStatusCode = ResponseStatusCode.Unauthorized;
                return result;
            }

            var chat = new ChatEntity()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Admin = currentUser,
            };

            var member = new MemberEntity()
            {
                Id = (Guid) currentUser,
                ChatId = chat.Id
            };

            await _chatRepository.Create(chat);
            await _memberRepository.Create(member);

            result = _mapper.Map<ResultContainer<ChatResponseDto>>(chat);
            result.ResponseStatusCode = ResponseStatusCode.Ok;

            return result;
        }

        public async Task<ResultContainer> DeleteChat(Guid id)
        {
            var result = new ResultContainer();
            var chat = await _chatRepository.GetById(id);

            if (chat == null)
            {
                result.ResponseStatusCode = ResponseStatusCode.NotFound;
                return result;
            }

            await _chatRepository.Delete(chat);

            result.ResponseStatusCode = ResponseStatusCode.NoContent;

            return result;
        }

        public async Task<ResultContainer<ChatUpdateResponseDto>> EditChat(ChatUpdateRequestDto data, Guid id)
        {
            var result = new ResultContainer<ChatUpdateResponseDto>();
            
            var chat = await _chatRepository.GetById(id);

            if (chat == null)
            {
                result.ResponseStatusCode = ResponseStatusCode.NotFound;
                return result;
            }

            chat.Title = data.NewTitle;

            result = _mapper.Map<ResultContainer<ChatUpdateResponseDto>>(await _chatRepository.Update(chat));

            result.ResponseStatusCode = ResponseStatusCode.Ok;
            return result;
        }

        public async Task<ResultContainer<ChatResponseDto>> GetChatById(Guid id)
        {
            var result = new ResultContainer<ChatResponseDto>();
            var chat = await _chatRepository.GetById(id);

            if (chat == null)
            {
                result.ResponseStatusCode = ResponseStatusCode.NotFound;
            }

            result = _mapper.Map<ResultContainer<ChatResponseDto>>(chat);

            result.ResponseStatusCode = ResponseStatusCode.Ok;

            return result;
        }

        public async Task<ResultContainer> AddMember(Guid userId, Guid chatId)
        {
            var result = new ResultContainer();
            var chat = await _chatRepository.GetById(chatId);

            if (chat == null)
            {
                result.ResponseStatusCode = ResponseStatusCode.NotFound;
                return result;
            }

            var member = new MemberEntity()
            {
                UserId = userId,
                ChatId = chatId
            };

            var memberInUse = await _memberRepository.GetByFilter(u => u.UserId == userId);

            if (memberInUse.Count > 0)
            {
                result.ResponseStatusCode = ResponseStatusCode.BadRequest;
                return result;
            }

            await _memberRepository.Create(member);

            result.ResponseStatusCode = ResponseStatusCode.NoContent;
            return result;
        }
    }
}