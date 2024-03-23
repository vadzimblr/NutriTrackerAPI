using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Controllers.Extensions;

public static class HttpContextExtension
{
    public static string GetUserId(this HttpContext httpContext)
    {
        return httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}