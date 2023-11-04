﻿using ErrorOr;
using PM.Application.Features.AuthContext.Dtos;
using PM.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace PM.Application.Common.Interfaces.ISercices;

public interface IIdentityService
{
    Task<bool> IsEmailExistAsync(string email);

    Task<bool> IsRoleExistAsync(string email);

    Task<ErrorOr<User>> RegisterAsync(
        string password,
        string roleName,
        User user);

    Task<ErrorOr<User>> UpdateAsync(
        User employee,
        CancellationToken cancellationToken);

    Task<ErrorOr<AuthResult>> LoginAsync(
        string email,
        string password);

    Task LogOutAsync(int userId);

    Task<ErrorOr<AuthResult>> RefreshAccessTokenAsync(string refreshToken);
}
