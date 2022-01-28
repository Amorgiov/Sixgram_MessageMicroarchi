using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Message.Core.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly HttpContext _httpContext;

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }
        
        public Guid? GetCurrentUserId() => 
            Guid.TryParse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
                ? userId : null;
    }
}