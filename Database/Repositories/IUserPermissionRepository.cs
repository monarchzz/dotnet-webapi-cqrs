namespace Database.Repositories;

public interface IUserPermissionRepository
{
    Task<bool> HasPermission(Guid userId, string permission);
}