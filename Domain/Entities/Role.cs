namespace Domain.Entities;

public class Role : BaseEntity
{
    public Role(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}