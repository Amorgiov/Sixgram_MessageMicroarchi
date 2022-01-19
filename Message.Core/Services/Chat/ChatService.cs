using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Message.Common.Enums;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Chat;
using Message.Database.Models;
using Message.Database.Repository.Chat;

namespace Message.Core.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        public ChatService(IMapper mapper, IChatRepository chatRepository)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
        }

        public async Task<ResultContainer<ChatDto>> CreateChat(ChatEntity model)
        {
            var result = _mapper.Map<ResultContainer<ChatDto>>(await _chatRepository.Create(model));
            return result;
        }

        public async Task<ResultContainer<ChatDto>> DeleteChat(int id)
        {
            var result = new ResultContainer<ChatDto>();
            var chat = await _chatRepository.Delete(id);
            if (chat == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<ChatDto>>(chat);
            return result;
        }

        public async Task<ResultContainer<ChatDto>> EditChat(ChatDto data)
        {
            var result = new ResultContainer<ChatDto>();
            var chat = _chatRepository.GetOne(_ => _.Title == data.Title &&
                                                   _.Members == data.Members &&
                                                   _.Admin == data.Admin);//bad case
            if (chat == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            chat.Title = data.Title;
            chat.Members = data.Members;
            chat.Admin = data.Admin;

            result = _mapper.Map<ResultContainer<ChatDto>>(await _chatRepository.Update(chat));
            return result;
        }

        public async Task<ResultContainer<ChatDto>> GetChatById(int id)
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