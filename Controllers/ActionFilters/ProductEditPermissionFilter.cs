using Contracts.ServiceContracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Controllers.ActionFilters;

public class ProductEditPermissionFilter:IAsyncActionFilter
{
    private readonly IServiceManager _service;
    private readonly UserManager<User> _userManager;

    public ProductEditPermissionFilter(IServiceManager service, UserManager<User> userManager)
    {
        _service = service;
        _userManager = userManager;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var productId =(Guid)context.ActionArguments.SingleOrDefault(p => p.Value is Guid).Value;
        var isAdmin = context.HttpContext.User.IsInRole("Admin");
        var user = await _userManager.FindByNameAsync(context.HttpContext.User.Identity.Name);
        if (!await _service.Product.IsProductCreator(productId, user.Id) && !isAdmin)
        {
            context.Result = new ForbidResult();
            return;
        }
        await next();
    }
}