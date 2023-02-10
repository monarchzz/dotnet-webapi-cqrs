using System.Text.RegularExpressions;

namespace Domain.Common.Authorization;

public static class Permissions
{
    private static readonly Permission[] All =
    {
        new("View Users", Actions.View, Resources.Users, new List<string>() { Roles.Admin }),
        new("Search Users", Actions.Search, Resources.Users, new List<string>() { Roles.Admin }),
        new("Create Users", Actions.Create, Resources.Users, new List<string>() { Roles.Admin }),
        new("Update Users", Actions.Update, Resources.Users, new List<string>() { Roles.Admin }),
        new("Delete Users", Actions.Delete, Resources.Users, new List<string>() { Roles.Admin }),

        new("View UserRoles", Actions.View, Resources.UserRoles, new List<string>() { Roles.Admin }),
        new("Update UserRoles", Actions.Update, Resources.UserRoles, new List<string>() { Roles.Admin }),

        new("View Roles", Actions.View, Resources.Roles, new List<string>() { Roles.Admin }),
        new("Create Roles", Actions.Create, Resources.Roles, new List<string>() { Roles.Admin }),
        new("Update Roles", Actions.Update, Resources.Roles, new List<string>() { Roles.Admin }),
        new("Delete Roles", Actions.Delete, Resources.Roles, new List<string>() { Roles.Admin }),
        new("View Roles", Actions.Search, Resources.Roles, new List<string>() { Roles.Admin }),
    };

    public static IEnumerable<Permission> Admin { get; } =
        All.Where(p => p.Roles.Contains(Roles.Admin)).ToList();

    public static IEnumerable<Permission> Basic { get; } =
        All.Where(p => p.Roles.Contains(Roles.Basic)).ToList();
}

public record Permission(string Description, string Action, string Resource, List<string> Roles)
{
    public string Name => _nameFor(Action, Resource);

    public static string NameFor(string action, string resource) => _nameFor(action, resource);

    public static bool IsValid(string permission) => _isValid(permission);

    private static string _nameFor(string action, string resource)
    {
        var name = $"{nameof(Permissions)}.{resource}.{action}";

        if (!_isValid(name)) throw new Exception("Invalid permission name");
        return name;
    }

    private static bool _isValid(string permission)
    {
        var rg = new Regex(@"^" + nameof(Permissions) + @"\.[a-zA-Z]+\.[a-zA-Z]+$");
        if (!rg.IsMatch(permission)) return false;

        var parts = permission.Split('.');
        var action = parts[2];
        var resource = parts[1];

        return Actions.IsInActions(action) && Resources.IsInResources(resource);
    }
}