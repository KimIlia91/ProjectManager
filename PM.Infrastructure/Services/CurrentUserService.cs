using Microsoft.AspNetCore.Http;
using PM.Application.Common.Interfaces.ISercices;
using PM.Domain.Common.Constants;
using System.Security.Claims;

namespace PM.Infrastructure.Services;

/// <summary>
/// Service that provides information about the current user's identity and session.
/// </summary>
public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private int _userId;
    private bool _isSupervisor;

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

            _userId = -1;
            return _userId;
        }
    }

    /// <inheritdoc />
    public bool IsSupervisor
    {
        get
        {
            if (_httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == ClaimTypes.Role))
            {
                var roleClaims = _httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role);

                var role = roleClaims.FirstOrDefault(c => c.Value == RoleConstants.Supervisor);

                if (role is not null)
                {
                    _isSupervisor = true;
                    return _isSupervisor;
                }
            }

            _isSupervisor = false;
            return _isSupervisor;
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
