using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Chat;
using Message.Database.Models;

namespace Message.Core.Services.Chat
{
    public interface IChatService
    {
        Task<ResultContainer<ChatDto>> CreateChat(ChatEntity model);
        Task<ResultContainer<ChatDto>> DeleteChat(int id);
        Task<ResultContainer<ChatDto>> EditChat(ChatDto model);
        Task<ResultContainer<ChatDto>> GetChatById(int id);
    }
}