using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodOrder.Endpoint.Helpers;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        
        var error = new ErrorModel
        (
            string.Join(',',
                (context.ModelState.Values.SelectMany(t => t.Errors.Select(z => z.ErrorMessage))).ToArray())
        );
        
        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Result = new JsonResult(error);
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}