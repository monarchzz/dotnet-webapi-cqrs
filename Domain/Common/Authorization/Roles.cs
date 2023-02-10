namespace Domain.Common.Authorization;

public class Roles
{
    public const string Admin = nameof(Admin);
    public const string Basic = nameof(Basic);

    public static IEnumerable<string> DefaultRoles { get; } = new List<string>(new[]
    {
        Admin,
        Basic
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}