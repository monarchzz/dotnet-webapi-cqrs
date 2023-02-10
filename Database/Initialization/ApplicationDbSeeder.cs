using System.Reflection;
using Domain.Common.Authorization;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Initialization;

public class ApplicationDbSeeder
{
    public async Task SeedDatabaseAsync(
        User user,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        await SeedRolesAsync(dbContext, cancellationToken);
        await SeedAdminUserAsync(user, dbContext, cancellationToken);
    }

    public async Task SeedRawQueryAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Database.ExecuteSqlRawAsync(
            await File.ReadAllTextAsync(GetPath("Views", "vw_UserPermissions.sql"), cancellationToken),
            cancellationToken);
    }

    private async Task SeedRolesAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        foreach (var roleName in Roles.DefaultRoles)
        {
            var role = new Role(name: roleName, description: $"{roleName} Role");
            await dbContext.Roles.AddAsync(role, cancellationToken);

            switch (roleName)
            {
                case Roles.Basic:
                    await AssignPermissionsToRoleAsync(dbContext, Permissions.Basic, role, cancellationToken);
                    break;
                case Roles.Admin:
                {
                    await AssignPermissionsToRoleAsync(dbContext, Permissions.Admin, role, cancellationToken);

                    break;
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task AssignPermissionsToRoleAsync(
        DbContext dbContext,
        IEnumerable<Permission> permissions,
        BaseEntity role,
        CancellationToken cancellationToken
    )
    {
        await dbContext.AddRangeAsync(permissions.Select(p => new RoleClaim
        {
            RoleId = role.Id,
            Value = p.Name
        }), cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedAdminUserAsync(
        User user,
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        await dbContext.Users.AddAsync(user, cancellationToken);
        // Assign role to user
        var adminRole = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Admin, cancellationToken);
        if (adminRole != null)
        {
            await dbContext.UserRoles.AddAsync(new UserRole(userId: user.Id, roleId: adminRole.Id), cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private string GetPath(params string[] paths)
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (basePath != null)
        {
            var path = Path.Combine(basePath, Path.Combine(paths));
            return path;
        }

        return Path.Combine(paths);
    }
}