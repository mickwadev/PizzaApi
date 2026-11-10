using Microsoft.EntityFrameworkCore;
using PizzaApi.Entities;

namespace PizzaApi.Data;

public class PizzaDbContext(DbContextOptions<PizzaDbContext> options):DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PizzaDbContext).Assembly);
    }

    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<UserRefreshToken> RefreshTokens { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }
}
