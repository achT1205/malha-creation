using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Middlewares;

public class RetrictAccessMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var referrer = context.Request.Headers["Referrer"].FirstOrDefault();
        if (string.IsNullOrEmpty(referrer))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            await context.Response.WriteAsync("Can't reach the page");
            return;
        }
        else
        {
            await next(context);
        }
    }
}
