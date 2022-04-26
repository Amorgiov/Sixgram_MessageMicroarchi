using System;
using System.Threading.Tasks;
using Message.Common.Enums;
using Message.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    /// <inheritdoc />
    public class BaseController : Controller
    {
        /// <summary>
        /// ...
        /// </summary>
        /// <param name="task"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TM"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected async Task<ActionResult> ReturnResult<T, TM>(Task<T> task) where T : ResultContainer<TM>
        {
            var result = await task;

            if (result.ResponseStatusCode == null)
            {
                throw new ArgumentOutOfRangeException(nameof(result.ResponseStatusCode),
                    $"Property {nameof(result.ResponseStatusCode)} can not be null");
            }

            return result.ResponseStatusCode switch
            {
                ResponseStatusCode.Ok => Ok(result.Data),
                _ => StatusCode((int)result.ResponseStatusCode)
            };
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected async Task<ActionResult> ReturnResult(Task<ResultContainer> task)
        {
            var result = await task;

            if (result.ResponseStatusCode == null)
            {
                throw new ArgumentOutOfRangeException(nameof(result.ResponseStatusCode),
                    $"Property {nameof(result.ResponseStatusCode)} can not be null");
            }

            return StatusCode((int)result.ResponseStatusCode);
        }
    }
}