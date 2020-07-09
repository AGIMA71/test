using ABNLookup.Responses;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ABNLookup.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}
