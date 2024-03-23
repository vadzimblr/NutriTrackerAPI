using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.RequestFeatures;

namespace Controllers.ActionFilters;

public class WaterConsumptionParametersValidationFilter:IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("parameters", out var objectParameters) && (objectParameters is WaterConsumptionParameters parameters))
        {
            if (!parameters.ValidateAmountRange)
            {
                throw new RangeBadRequestException("Invalid amount range");
            }
        }
        else
        {
            throw new BadRequestException("Product parameters are missing");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}