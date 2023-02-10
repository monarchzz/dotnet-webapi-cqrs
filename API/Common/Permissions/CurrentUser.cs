using System.Security.Claims;
using Infrastructure.Extensions;

namespace API.Common.Permissions;

public class CurrentUser
{
    private readonly ClaimsPrincipal? _user;

    private readonly Guid _userId = Guid.Empty;

    public CurrentUser(ClaimsPrincipal? user = null)
    {
        _user = user;
    }

    public Guid GetUserId()
    {
        return IsAuthenticated()
            ? _user?.GetUserId() ?? _userId
            : _userId;
    }

    public string? GetUserEmail()
    {
        return IsAuthenticated()
            ? _user!.GetEmail()
            : string.Empty;
    }

    public bool IsAuthenticated()
    {
        return _user?.Identity?.IsAuthenticated is true;
    }
}

public static class CurrentUserExtensions
{
    public static CurrentUser GetCurrentUser(this ClaimsPrincipal user)
    {
        return new CurrentUser(user);
    }
}