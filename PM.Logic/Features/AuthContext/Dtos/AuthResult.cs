namespace PM.Application.Features.AuthContext.Dtos;

/// <summary>
/// Represents the result of an authentication operation.
/// </summary>
/// <param name="UserName">The username of the authenticated user.</param>
/// <param name="AccessToken">The access token issued upon successful authentication.</param>
/// <param name="RefreshToken">The refresh token used to obtain new access tokens.</param>
public sealed record AuthResult(
    int UserId,
    string UserName,
    string AccessToken,
    string RefreshToken,
    List<string> Roles);