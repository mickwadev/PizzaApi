using Microsoft.EntityFrameworkCore;
using PizzaApi.Data;

namespace PizzaApi.Extensions;

public static class PizzaApi
{
    extension(WebApplication webApplication)
    {
        public void MapPizzaApi()
        {
            var pizzaApi = webApplication.MapGroup("/pizza");
            pizzaApi.MapGet("/ingredients", async(PizzaDbContext dbContext) =>
            {
               var all = await dbContext.Ingredients.AsNoTracking().ToListAsync();
                return Results.Ok(all);
            }).RequireAuthorization();
        }
    }
}
