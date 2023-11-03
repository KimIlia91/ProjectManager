namespace PM.Application.Features.AuthContext.Dtos
{
    public sealed record LoginResult(
        string UserName,
        string AccessToken,
        string RefreshToken);
}