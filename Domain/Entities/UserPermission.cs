namespace Domain.Entities;

public class UserPermission
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public string Permission { get; set; } = string.Empty;
}