using System;

namespace Message.Core.Services.Token
{
    public interface ITokenService
    {
        Guid? GetCurrentUserId();
    }
}