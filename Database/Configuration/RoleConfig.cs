using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(role => role.Id);
        builder
            .Property(r => r.Name)
            .HasColumnType("nvarchar(500)");

        builder
            .Property(r => r.Description)
            .HasColumnType("nvarchar(1000)");

        builder.ToTable("Roles", table => table.IsTemporal());
    }
}