using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaApi.Entities;

namespace PizzaApi.Configurations;

public class RolesConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(role => role.Id);
        builder.HasIndex(role => role.Id);
        builder.Property(role => role.Name).HasMaxLength(42).IsRequired();

        builder.HasData(
            new UserRole { Id = 1, Name = "Admin" },
            new UserRole { Id = 2, Name = "Customer" }
            );

    }
}
