using ErrorOr;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.AuthContext.Dtos;
using PM.Infrastructure.Persistence;

namespace PM.Tests.Common.Interfaces;

public class FakeService : TestCommandBase, IIdentityService
{
    public async Task<ErrorOr<Domain.Entities.User>> CreateUserAsync(
        string password,
        Domain.Entities.User user)
    {
        await Context.AddAsync(user);
        await Context.SaveChangesAsync();
        return user;
    }

    public Task<bool> IsEmailExistAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsRoleExistAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<AuthResult>> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public System.Threading.Tasks.Task LogOutAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<AuthResult>> RefreshAccessTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Domain.Entities.User>> UpdateAsync(
        Domain.Entities.User employee,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ApplicationDbContext Get()
    {
        return Context;
    }
}
