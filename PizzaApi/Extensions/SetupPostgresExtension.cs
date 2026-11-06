using Microsoft.EntityFrameworkCore;
using Npgsql;
using PizzaApi.Data;

namespace PizzaApi.Extensions;

public static class SetupPostgresExtension
{
    extension(WebApplicationBuilder builder)
    {
        public void AddMyPostgresDbContext()
        {
            var dbPassword = builder.Configuration["db:password"] ?? throw new Exception("Missing db password");
            var csb = new NpgsqlConnectionStringBuilder(
            builder.Configuration.GetConnectionString("PizzaPostgresDb"))
            { 
                Password = builder.Configuration["db:password"]
            };
            var connectionString = csb.ConnectionString;
            builder.Services
                .AddDbContext<PizzaDbContext>(options => options
                .UseNpgsql(csb.ConnectionString)
#if DEBUG
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
#endif
                .UseSnakeCaseNamingConvention());
        }
    }
}
