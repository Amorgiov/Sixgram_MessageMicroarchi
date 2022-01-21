using Message.Core.Services.Message;
using Message.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    public class MessageController : BaseController
    {
        private readonly MessageService _message;
        /// <inheritdoc />
        public MessageController(MessageService message)
        {
            _message = message;
        }
    }
}