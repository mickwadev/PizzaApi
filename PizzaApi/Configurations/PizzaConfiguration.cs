using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaApi.Entities;

namespace PizzaApi.Configurations;

public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.HasOne(pizza => pizza.spicy).WithMany( sl => sl.pizzas).HasForeignKey(pizza => pizza.SpicyLevelId);
        builder.HasData(
            new Pizza()
            {
                Id = 1,
                Name = "Margherita",
                SpicyLevelId = 1,
                Price = 12.12M,
                Stars = 4
            },
            new Pizza()
            {
                Id = 2,
                Name = "Fungi",
                SpicyLevelId = 1,
                Price = 12.34M,
                Stars = 5
            },
            new Pizza()
            {
                Id = 3,
                Name = "4 sery",
                SpicyLevelId = 2,
                Price = 22.32M,
                Stars = 5
            },
            new Pizza()
            {
                Id = 4,
                Name = "Piekielne papryczki",
                SpicyLevelId = 4,
                Price = 72.44M,
                Stars = 5
            });
    }
}
