using System;
using System.Threading.Tasks;
using Message.Common.Enums;
using Message.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    public class BaseController : Controller
    {
        protected async Task<ActionResult> ReturnResult<T, TM>(Task<T> task) where T : ResultContainer<TM>
        {
            var result = await task;

            if (result.ErrorType.HasValue)
            {
                return result.ErrorType switch
                {
                    ErrorType.Unauthorized => Unauthorized(),
                    ErrorType.BadRequest => BadRequest(),
                    ErrorType.NotFound => NotFound(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            if (result.Data == null)
                return NoContent();
            
            return Ok(result.Data);
        }
    }
}