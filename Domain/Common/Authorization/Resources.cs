namespace Domain.Common.Authorization;

public static class Resources
{
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);

    private static readonly IEnumerable<string> Values = new[]
    {
        Users,
        UserRoles,
        Roles,
        RoleClaims,
    };

    public static bool IsInResources(string resource)
    {
        return Values.Contains(resource);
    }
}