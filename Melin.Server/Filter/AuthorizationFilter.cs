using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Melin.Server.Filter;

public class AuthorizationFilter : IActionFilter
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public AuthorizationFilter(ProblemDetailsFactory factory)
    {
        _problemDetailsFactory = factory;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.Identity != null)
        {
            return;
        }
        
        var problemDetails = _problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);
        context.Result = new UnauthorizedObjectResult(problemDetails);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}