namespace Infrastructure.Authentication;

public class JwtSettings
{
    public string RefreshSecret { get; set; } = null!;

    public string Secret { get; init; } = null!;

    public string Issuer { get; init; } = null!;

    public string Audience { get; init; } = null!;

    public int ExpiryMinutes { get; init; }

    public int RefreshExpiryMinutes { get; init; }
}