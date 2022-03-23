using System;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto.Chat;
using Message.Core.Dto.Message;
using Message.Core.Dto.Update;
using Message.Core.Services.Message;
using Message.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    [ApiController]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        /// <inheritdoc />
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Creating message and sending to heap
        /// </summary>
        /// <param name="data"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        [HttpPost("[action]/{chatId:guid}")]
        public async Task<ActionResult<MessageDto>> AddMessage(MessageDto data, Guid chatId)
            => await ReturnResult<ResultContainer<MessageDto>, MessageDto>(_messageService.AddMessage(data, chatId));
    }
}