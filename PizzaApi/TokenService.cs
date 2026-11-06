using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PizzaApi.Data;
using PizzaApi.Entities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PizzaApi;

// This creates JWT and refresh token
// Token will be stored in db
public class TokenService(PizzaDbContext dbContext,
    IConfiguration configuration)
{

    public (string jwt, string refreshToken) Generate(User user)
    {
        var jwt = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();
        return (jwt, refreshToken);
    }

    private string GenerateAccessToken(User user)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new("Grzybek","Borowik"), // it looks like anything can be added here
            new(ClaimTypes.Role,"Customer")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secret"]?? throw new Exception("Missing secret key for jwt")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
           // Expires = issuedAt.AddMinutes(30),
            SigningCredentials = credentials,
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };
        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }

    private string GenerateRefreshToken()
    {
        // Refresh token does not have any date time inside
        using var g = RandomNumberGenerator.Create();
        var random = new byte[10];
        g.GetBytes(random);
        return Convert.ToBase64String(random);
    }


}
