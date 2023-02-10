using Database;
using Database.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UserPermissionRepository : IUserPermissionRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<UserPermission> _dbSet;

    public UserPermissionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<UserPermission>();
    }

    public Task<bool> HasPermission(Guid userId, string permission)
    {
        return _dbSet.AsNoTracking().AnyAsync(up => up.UserId == userId && up.Permission == permission);
    }
}