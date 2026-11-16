using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaApi.Entities;

namespace PizzaApi.Configurations
{
    public class SpicyLevelsConfiguration : IEntityTypeConfiguration<SpicyLevel>
    {
        public void Configure(EntityTypeBuilder<SpicyLevel> builder)
        {
            builder.HasData(
                new SpicyLevel() { Id = 1, Description = "Łagodny jak Szczerbuś :)", Lvl =0 },
                new SpicyLevel() { Id = 2, Description = "Lekko ostra", Lvl=1 },
                new SpicyLevel() { Id = 3, Description = "Średnio ostra", Lvl = 2 },
                new SpicyLevel() { Id = 4, Description = "Bardzo ostra >.<", Lvl = 5 }
                );
        }
    }
}
