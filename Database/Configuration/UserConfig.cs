using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Email);

        builder.Property(user => user.FirstName).HasColumnType("nvarchar(100)");
        builder.Property(user => user.LastName).HasColumnType("nvarchar(100)");
        builder.Property(user => user.Email).HasColumnType("nvarchar(200)");

        builder.ToTable("Users", table => table.IsTemporal());
    }
}