using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Data;
using PizzaApi.DTOs;
using PizzaApi.Entities;

namespace PizzaApi.Extensions;

public static class AuthApi
{
    extension(WebApplication webApplication)
    {
        public void MapAuthApi()
        {
            var auth = webApplication.MapGroup("/auth");

            auth.MapPost("/create", async (CreateUserRequestDto request, PizzaDbContext dbContext) => 
            {
                // First check if no such user is in db:
                User? foundUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == request.Name);

                if (foundUser is not null)
                {
                    return Results.BadRequest($"User {request.Name} already exists");
                }

                // no found user? Create one:
                User newUser = new User();
                // this hash is different each time!
                var hashedPassword = new PasswordHasher<User>().HashPassword(newUser,request.Password);
                newUser.Name = request.Name;
                newUser.HashedPassword = hashedPassword;
                dbContext.Users.Add(newUser);
                await dbContext.SaveChangesAsync();
                return Results.Ok($"User {request.Name} created OK");
            });

            auth.MapPost("/login", (LoginUserRequestDto request, PizzaDbContext dbContext) => 
            { 
            });
        }
    }
}
