using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace BuildingBlocks.Middlewares;
public class RetrictAccessMiddleware
{
    private readonly RequestDelegate _next;

    public RetrictAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Vérification si l'appel est interne
        if (IsInternalRequest(context))
        {
            // Appel interne, pas de validation du token
            await _next(context);
            return;
        }

        // Récupération du token pour les appels externes
        var token = context.Request.Headers["Internal-Token"].FirstOrDefault();

        if (string.IsNullOrEmpty(token) || !ValidateJwtToken(token))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access forbidden: Not from Gateway");
            return;
        }

        await _next(context);
    }

    private bool IsInternalRequest(HttpContext context)
    {
        // Critère 1 : Vérification de l'IP (à adapter selon votre configuration réseau)
        var remoteIp = context.Connection.RemoteIpAddress;

        // Exemple de vérification pour IPv4 local ou une plage interne spécifique
        if (remoteIp != null && (IPAddress.IsLoopback(remoteIp) || IsInInternalRange(remoteIp)))
        {
            return true;
        }

        return false;
    }

    private bool IsInInternalRange(IPAddress remoteIp)
    {
        // Vous pouvez ajouter des vérifications plus détaillées ici
        // Exemple pour une plage 10.0.0.0/24
        byte[] bytes = remoteIp.GetAddressBytes();
        return bytes[0] == 10;
    }

    private bool ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("JvG5TCdYB6ZGP2ePRtz6frEl9YF1YNmL3b+9lR9SIFQ=");

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = "Api-Gateway",
                ValidAudience = "Internal-Services",
                ValidateIssuer = true,
                ValidateAudience = true
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
