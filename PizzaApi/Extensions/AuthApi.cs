using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Data;
using PizzaApi.DTOs;
using PizzaApi.Entities;
using PizzaApi.Migrations;

namespace PizzaApi.Extensions;

public static class AuthApi
{
    private static UserRefreshToken CreateToken(string refreshToken, int userId)
    {
        return new()
        {
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            DeviceInfo = "Test", // later Maui app or web
            HashedToken = TokenService.HashRefreshToken(refreshToken),
            Revoked = false,
            UserId = userId
        };
    }

    extension(WebApplication webApplication)
    {
        public void MapAuthApi()
        {
            var auth = webApplication.MapGroup("/auth");

            auth.MapPost("/create", async Task<Results<BadRequest<string>, Ok<string>>> (CreateUserRequestDto request, PizzaDbContext dbContext) => 
            {
                // First check if no such user is in db:
                User? foundUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == request.Name);

                if (foundUser is not null)
                {
                    return TypedResults.BadRequest($"User {request.Name} already exists");
                }

                // no found user? Create one:
                User newUser = new User();
                // this hash is different each time!
                var hashedPassword = new PasswordHasher<User>().HashPassword(newUser,request.Password);
                newUser.Name = request.Name;
                newUser.HashedPassword = hashedPassword;
                newUser.MyRoleId = request.RoleId;
                dbContext.Users.Add(newUser);
                await dbContext.SaveChangesAsync();
                return TypedResults.Ok($"User {request.Name} created OK");
            });

            // 401 - kiedy login/hasło jest złe
            auth.MapPost("/login", async Task<Results<Ok<LoginUserResponseDto>, UnauthorizedHttpResult>> (
                LoginUserRequestDto request,
                TokenService tokenService,
                PizzaDbContext dbContext) => 
            {
                User? exisingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == request.Name);
                // if user does not exist, fake password check anyway for security reason,
                // so attacker does know if user exists or not by responce time
                string hashToCheck = exisingUser != null? exisingUser.HashedPassword : "some random string";
                var result = new PasswordHasher<User>().VerifyHashedPassword(exisingUser, hashToCheck, request.Password);

                if(result != PasswordVerificationResult.Success)
                {
                    return TypedResults.Unauthorized();
                }

                var tokens = tokenService.Generate(exisingUser);
                UserRefreshToken urt = CreateToken(tokens.refreshToken, exisingUser.Id);

                dbContext.Add(urt);
                await dbContext.SaveChangesAsync();
                return TypedResults.Ok(new LoginUserResponseDto(             
                    tokens.jwt,
                    tokens.refreshToken
                ));
            });


            // 401 - kiedy refresh token jest niepoprawny
            // todo: kiedy będzie drugi raz użyty ten sam refresh token anulować wszystkie tokeny
            auth.MapPost("/refresh", async Task<Results<UnauthorizedHttpResult,Ok<LoginUserResponseDto>>> (RefreshUserRequestDto request,
                PizzaDbContext dbContext, TokenService tokenService) =>
            {
                // Check if there is not used, not outdated rt in db:
                var hashedRefreshToken = TokenService.HashRefreshToken(request.RefreshToken);
                DateTime now = DateTime.UtcNow;
                UserRefreshToken? validToken = await dbContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.HashedToken == hashedRefreshToken && rt.Revoked == false &&
                 rt.ExpiresAt > now);

                if (validToken is null)
                {
                    return TypedResults.Unauthorized();
                }
                // if there is found token, search for its owner, create and return new tokens
                User tokenOwner = await dbContext.Users.FindAsync(validToken.UserId);
                var tokens =  tokenService.Generate(tokenOwner);
                var rt = CreateToken(tokens.refreshToken, tokenOwner.Id);
                validToken.Revoked = true;
                dbContext.RefreshTokens.Add(rt);
                await dbContext.SaveChangesAsync();
                return TypedResults.Ok(new LoginUserResponseDto(tokens.jwt,tokens.refreshToken));
            });

            auth.MapDelete("/delete", async () => 
            { 
            });

            auth.MapGet("/roles", async (PizzaDbContext dbContext) => 
            {
                var roles = await dbContext.UserRoles.AsNoTracking().ToListAsync();
                return TypedResults.Ok(roles.Select(r => new UserRoleResponseDto(r.Id, r.Name)));
            });
        }
    }
}
