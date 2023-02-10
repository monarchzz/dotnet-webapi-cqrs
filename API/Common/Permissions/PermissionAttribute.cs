using Domain.Common.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace API.Common.Permissions;

public class PermissionAttribute : AuthorizeAttribute
{
    public PermissionAttribute(string action, string resource) =>
        Policy = Permission.NameFor(action, resource);
}