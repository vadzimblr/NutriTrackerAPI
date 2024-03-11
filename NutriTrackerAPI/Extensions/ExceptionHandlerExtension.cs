using System.Net;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace NutriTrackerAPI.Extensions;

public static class ExceptionHandlerExtension
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(opt =>
        {
            opt.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    await context.Response.WriteAsync(new ExceptionDetails
                    {
                        StatusCode = context.Response.StatusCode.ToString(),
                        Message = contextFeature.Error.Message
                    }.ToString());
                }
            });
        });
    }
}