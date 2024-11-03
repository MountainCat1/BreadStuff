using BreadChat.Application.ApplicationErrors;

namespace BreadChat.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundError error)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(new { error.Message });
        }
    }
}