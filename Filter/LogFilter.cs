using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Filter;

public class LogFilter : IActionFilter
{
    private readonly ILogger _logger;
    private readonly JwtManager _jwtManager;
    public LogFilter(ILogger<LogFilter> logger,JwtManager jwtManager)
    {
        _logger = logger;
        _jwtManager =jwtManager ;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var principal = _jwtManager.VerifyJwt(context.HttpContext.Request.Headers["Authorization"]!);
        if (principal == null)
        {
            context.Result =new ForbidResult();
        }
        var userEmail = principal?.FindFirst(ClaimTypes.Email);
        context.HttpContext.Request.Headers["email"] = userEmail?.Value;
        var id = principal?.FindFirst(JwtRegisteredClaimNames.Sid);
        context.HttpContext.Request.Headers["id"] = id?.Value;
        var name = principal?.FindFirst(ClaimTypes.Name);
        context.HttpContext.Request.Headers["name"] = name?.Value;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Action end: {actionName}", context.ActionDescriptor.DisplayName);
    }
}
