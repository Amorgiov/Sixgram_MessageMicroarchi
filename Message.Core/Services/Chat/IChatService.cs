using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Chat;
using Message.Core.Dto.Update;
using Message.Database.Models;

namespace Message.Core.Services.Chat
{
    public interface IChatService
    {
        Task<ResultContainer<ChatResponseDto>> CreateChat(ChatRequestDto model);
        Task<ResultContainer> DeleteChat(Guid id);
        Task<ResultContainer<ChatUpdateResponseDto>> EditChat(ChatUpdateRequestDto model, Guid id);
        Task<ResultContainer<ChatResponseDto>> GetChatById(Guid id);
        Task<ResultContainer> AddMember(Guid userId, Guid chatId);
        
    }
}