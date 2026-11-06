using PizzaApi.Data;
using PizzaApi.DTOs;

namespace PizzaApi.Extensions;

public static class AuthApi
{
    extension(WebApplication webApplication)
    {
        public void MapAuthApi()
        {
            var auth = webApplication.MapGroup("/auth");

            auth.MapPost("/create", (CreateUserRequestDto request, PizzaDbContext dbContext) => 
            { 
                // First check if no such user is in db:
                
            });
        }
    }
}
