using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CoreAPI.Attributes
{
    public class GenerateAntiForgeryCookie : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var antiForgery = context.HttpContext.RequestServices.GetService<IAntiforgery>();
            var tokens = antiForgery.GetAndStoreTokens(context.HttpContext);
            context.HttpContext.Response.Cookies.Append(
                "XSRF-TOKEN",
                tokens.RequestToken,
                new Microsoft.AspNetCore.Http.CookieOptions() { HttpOnly = false });
        }
    }
}
