using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OcelotApiGateway.Middlewares;
public class InterceptionMiddleware
{
    private readonly RequestDelegate _next;

    public InterceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Génération du JWT
        var token = GenerateJwtToken();

        // Ajout de l’en-tête
        context.Request.Headers["Internal-Token"] = token;

        await _next(context);
    }

    private string GenerateJwtToken()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JvG5TCdYB6ZGP2ePRtz6frEl9YF1YNmL3b+9lR9SIFQ="));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "Api-Gateway",
            audience: "Internal-Services",
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}