using Serilog.Context;

namespace Melin.Server.Middleware;

public class SerilogConfigurationMiddleware
{
    private readonly RequestDelegate _next;

    public SerilogConfigurationMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public Task Invoke(HttpContext context)
    {
        // Add userEmail to logs
        var userEmail = "";
        if (context.User.Identity != null)
        {
            userEmail += context.User.Identity.Name;
        }

        LogContext.PushProperty("UserEmail", userEmail);
        
        // Add Request Endpoint to logs
        var endpointMethod = context.Request.Method;
        LogContext.PushProperty("HTTPMethod:", endpointMethod);
        
        var endpoint = context.GetEndpoint();
        LogContext.PushProperty("Endpoint:", endpoint);

        var host = context.Request.Host;
        LogContext.PushProperty("Host:", host);

        return _next(context);
    }
}