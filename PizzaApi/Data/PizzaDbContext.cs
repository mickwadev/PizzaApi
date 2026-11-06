using Microsoft.EntityFrameworkCore;
using PizzaApi.Entities;

namespace PizzaApi.Data;

public class PizzaDbContext(DbContextOptions<PizzaDbContext> options):DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PizzaDbContext).Assembly);
    }

    public DbSet<Ingredient> Ingredients { get; set; }
}
