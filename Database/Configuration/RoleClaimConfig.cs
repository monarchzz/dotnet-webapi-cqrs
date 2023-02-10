using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration;

public class RoleClaimConfig : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.HasKey(rc => rc.Id);
        builder
            .Property(rc => rc.Value).HasColumnType("nvarchar(200)");
        builder
            .HasOne(rc => rc.Role).WithMany(r => r.RoleClaims).HasForeignKey(rc => rc.RoleId);

        builder.ToTable("RoleClaims", table => table.IsTemporal());
    }
}