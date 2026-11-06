using Microsoft.EntityFrameworkCore;
using PizzaApi.Entities;

namespace PizzaApi.Data;

public class PizzaDbContext(DbContextOptions<PizzaDbContext> options):DbContext(options)
{
    public DbSet<Ingredient> Ingredients { get; set; }
}
