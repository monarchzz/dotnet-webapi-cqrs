using Database.Repositories;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace API.Common.Permissions;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserPermissionRepository _userPermissionRepository;

    public PermissionAuthorizationHandler(IUserPermissionRepository userPermissionRepository)
    {
        _userPermissionRepository = userPermissionRepository;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement
    )
    {
        if (context.User.GetUserId() is { } userId && await _hasPermissionAsync(userId, requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }

    private Task<bool> _hasPermissionAsync(Guid userId, string permission)
    {
        return _userPermissionRepository.HasPermission(userId, permission);
    }
}