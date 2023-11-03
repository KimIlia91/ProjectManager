using Microsoft.AspNetCore.Http;
using PM.Application.Common.Interfaces.ISercices;
using System.Security.Claims;

namespace PM.Infrastructure.Services
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int? _userId;

        public int? UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                {
                    var userId = int.Parse(
                        _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                    if (userId > 0)
                    {
                        _userId = userId;

                        return _userId;
                    }
                }

                return -1;
            }
        }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
