using ICommerce.Contracts;
using Serilog;

namespace ICommerce.Catalog.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(ex, httpContext);
        }
    }

    private async Task HandleException(Exception ex, HttpContext httpContext)
    {

        Log.Error(ex, "Error happend!");

        if (ex is InvalidOperationException)
        {
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsJsonAsync(new ApiResponse<string>
            {
                Message = "Invalid operation",
                StatusCode = 400,
                 Data = null
            });
        }
        else if (ex is ArgumentException)
        {
            await httpContext.Response.WriteAsync("Invalid argument");
        }
        else
        {
            await httpContext.Response.WriteAsync("Unknown error");
        }


    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}