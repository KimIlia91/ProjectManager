using Microsoft.AspNetCore.Http;
using PM.Application.Common.Interfaces.ISercices;
using System.Security.Claims;

namespace PM.Infrastructure.Services;

/// <summary>
/// Service that provides information about the current user's identity and session.
/// </summary>
public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private int _userId;

    /// <inheritdoc />
    public int UserId
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

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUserService"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor used 
    /// to access the current HTTP context.</param>
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}
