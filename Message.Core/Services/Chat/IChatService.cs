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
        Task<ResultContainer<ChatDto>> CreateChat(ChatEntity model);
        Task<ResultContainer<ChatUpdateResponseDto>> DeleteChat(Guid id);
        Task<ResultContainer<ChatUpdateResponseDto>> EditChat(ChatUpdateRequestDto model, Guid id);
        Task<ResultContainer<ChatDto>> GetChatById(Guid id);
    }
}