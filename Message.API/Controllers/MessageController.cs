using System;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto.Message;
using Message.Core.Services.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    /// <inheritdoc />
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/")]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private const long MaxFileSize = 2L * 1024L * 1024L * 1024L;
        /// <inheritdoc />
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        /// <summary>
        /// Creating messages and files
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <response code="204">Success</response>
        /// <response code="400">There is no file in the request</response>
        [HttpPost("chat/{id:guid}/message")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        public async Task<ActionResult> AddMessage([FromForm] CreateMessageDto data, Guid id)
            => await ReturnResult(_messageService.AddMessage(data, id));
        
        /// <summary>
        /// Get message by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="400">There is no file in the request</response>
        [HttpGet("message/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<CreateMessageDto>> GetMessageById(Guid id)
            => await ReturnResult<ResultContainer<CreateMessageDto>, CreateMessageDto>
                (_messageService.GetMessageById(id));

        /// <summary>
        /// Delete messages by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="400">There is no file in the request</response>
        /// <response code="200">Success</response>
        /// <response code="404">Message not found</response>
        [HttpDelete("message/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(Guid id)
            => await ReturnResult(_messageService.Delete(id));
    }
}