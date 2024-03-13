using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.RequestFeatures;

namespace Controllers.ActionFilters;

public class ProductParametersValidationFilter:IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var parameters = (ProductParameters)context.ActionArguments.SingleOrDefault(p => p.Value is ProductParameters).Value;
        if (!parameters.ValidateCaloriesRange)
            throw new RangeBadRequestException("Invalid Calories range");
        if (!parameters.ValidateFatRange)
            throw new RangeBadRequestException("Invalid Fat range");
        if (!parameters.ValidateProteinRange)
            throw new RangeBadRequestException("Invalid Protein range");
        if (!parameters.ValidateCarbsRange)
            throw new RangeBadRequestException("Invalid Carbs range");
        if (!parameters.ValidateServingSizeRange)
            throw new RangeBadRequestException("Invalid Serving Size range");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}