namespace server.Services;

public interface IJwtService
{
    string GenerateJwt(Guid Id, bool isRefreshToken);

    string VerifyRefreshToken(string oldToken);
}