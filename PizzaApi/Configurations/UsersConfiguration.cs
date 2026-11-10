using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaApi.Entities;

namespace PizzaApi.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Id);

        // To można też zdefiniować w UserRole, ale należy tylko w 1 miejscu, lepiej w relacji zależnej, tej która ma FK
        builder.HasOne(user => user.MyRole)
            .WithMany(role => role.Users)
            .HasForeignKey(user => user.MyRoleId)
            .IsRequired()
            // Restrict - nie można usunąć tej roli jeśli ktoś ją ma przypisaną:
            .OnDelete(DeleteBehavior.Restrict);
        // mam już kilku użytkowników, bez tego nie ustawi roli która jest required
        builder.Property(user => user.MyRoleId).HasDefaultValue(1);
    }
}
