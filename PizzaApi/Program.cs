
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PizzaApi.Extensions;
using Scalar.AspNetCore;
using System.Text;

namespace PizzaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                // https://milanjovanovic.tech/blog/jwt-authentication-aspnetcore
            }).AddJwtBearer(options =>
            {
                // z tym trzeba uważać, bo to wpływa na mapowanie nazw i nie działa potem sub czy coś...
                options.MapInboundClaims = false;
                var jwtSettings = builder.Configuration.GetSection("JwtSettings");
                var secret = builder.Configuration["jwt:secret"] ?? throw new Exception("Missing jwt secret");
                // https://www.youtube.com/watch?v=3S8wQnquxp8 
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ClockSkew = TimeSpan.Zero // Remove delay of token expiration
                };
            });

            builder.Services.AddOpenApi();
            builder.AddMyPostgresDbContext();
            builder.Services.AddScoped<TokenService>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            // This will be executed on each api call
            app.Use(async (context, next) => 
            {
              //  await context.Response.WriteAsync("test");
                await next.Invoke(context);
            }); 
            app.UseAuthorization();
            app.MapPizzaApi();
            app.MapAuthApi();
            app.Run();
        }
    }
}
