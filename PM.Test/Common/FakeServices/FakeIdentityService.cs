using ErrorOr;
using MapsterMapper;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.AuthContext.Dtos;
using PM.Infrastructure.Persistence;
using PM.Test.Common.Data;

namespace PM.Test.Common.FakeServices;

public class FakeIdentityService : IIdentityService
{
    protected readonly ApplicationDbContext Context;
    protected readonly IMapper Mapper;

    public FakeIdentityService()
    {
        Context = ApplicationDbContextFactory.Create();
        Mapper = new Mapper();
    }

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

    public Task LogOutAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<AuthResult>> RefreshAccessTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorOr<Domain.Entities.User>> UpdateAsync(
        Domain.Entities.User user,
        CancellationToken cancellationToken)
    {
        Context.Update(user);
        await Context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public ApplicationDbContext Get()
    {
        return Context;
    }
}
