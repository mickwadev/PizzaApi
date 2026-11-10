using Microsoft.EntityFrameworkCore;
using PizzaApi.Data;
using PizzaApi.DTOs;

namespace PizzaApi.Extensions;

public static class PizzaApi
{
    extension(WebApplication webApplication)
    {
        public void MapPizzaApi()
        {
            var pizzaApi = webApplication.MapGroup("/pizza");
            pizzaApi.MapGet("/ingredients", async (PizzaDbContext dbContext) =>
            {
                var all = await dbContext.Ingredients.AsNoTracking().ToListAsync();
                return TypedResults.Ok(all.Select(ing => new IngredientDto(ing.Name)));
            }).RequireAuthorization();

            pizzaApi.MapGet("/panmors", () =>
            {
                return TypedResults.Ok("Pan Mors :3=");
            });

            pizzaApi.MapGet("/ziemniaczek", () => 
            {
                return TypedResults.Ok("ziemniaczek 🥔 ^_^");
            });
        }
    }
}

            