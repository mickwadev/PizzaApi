using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaApi.Entities;

namespace PizzaApi.Configurations;

public class IngredientsConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasData(
            new Ingredient()
            {
                Id = 1,
                Name = "żółty ser"
            },
            new Ingredient
            {
                Id = 2,
                Name = "sos pomidorowy"
            },
           new
           {
               Id = 3,
               Name = "pieczarki"
           });
    }
}
