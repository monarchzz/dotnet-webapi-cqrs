using Domain.Entities;

namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);

    public string GenerateRefreshToken(User user);

    public Guid? VerifyRefreshToken(string refreshToken);
}