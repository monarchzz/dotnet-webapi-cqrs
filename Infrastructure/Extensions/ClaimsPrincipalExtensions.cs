using System.Security.Claims;
using Domain.Common.Authorization;

namespace Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimTypes.Email);

    public static Guid? GetUserId(this ClaimsPrincipal principal)
    {
        var str = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (str is null) return null;

        return Guid.TryParse(str, out var id) ? id : null;
    }

    public static DateTimeOffset GetExpiration(this ClaimsPrincipal principal) =>
        DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(
            principal.FindFirstValue(Claims.Expiration)));

    private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value;
}