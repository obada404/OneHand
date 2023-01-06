﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Filter;

public class LogFilter : IActionFilter
{
    private readonly ILogger _logger;
    private readonly JwtManager _jwtManager;
    public LogFilter(ILogger<LogFilter> logger,IConfiguration config)
    {
        _logger = logger;
        _jwtManager = new JwtManager(config["Jwt:Key"]);
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var principal = _jwtManager.VerifyJwt(context.HttpContext.Request.Headers["Authorization"]!);
        if (principal == null)
        {
            context.Result =new ForbidResult();
        }
        

        var userEmail = principal?.FindFirst(JwtRegisteredClaimNames.Email);
        context.HttpContext.Request.Headers["email"] = userEmail?.Value;
        var role = principal?.FindFirst(ClaimTypes.Role)?.Value;
        context.HttpContext.Request.Headers["role"] = role;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Action end: {actionName}", context.ActionDescriptor.DisplayName);
    }
}
