using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Controllers.ActionFilters;

public class ValidationFilter:IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var validationObject = context.ActionArguments.SingleOrDefault(p => p.Key.Contains("Dto")).Value;
        if (validationObject is null)
        {
            context.Result = new BadRequestObjectResult("Provided object is null.");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
            
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}