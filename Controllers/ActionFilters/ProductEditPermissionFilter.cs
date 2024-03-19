using System.Security.Claims;
using Contracts.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Controllers.ActionFilters
{
    public class ProductEditPermissionFilter : IAsyncActionFilter
    {
        private readonly IServiceManager _service;
        private string? _userId;

        public ProductEditPermissionFilter(IServiceManager service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                _userId = userIdClaim.Value;
            }

            if (_userId == null)
            {
                context.Result = new ForbidResult();
                return;
            }
            
            if (!context.ActionArguments.TryGetValue("productId", out var productIdObj) || !(productIdObj is Guid productId))
            {
                context.Result = new BadRequestResult();
                return;
            }

            var isAdmin = context.HttpContext.User.IsInRole("Admin");

            if (!await _service.Product.IsProductCreator(productId, _userId) && !isAdmin)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}