using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SuperMarketAPI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["Api-Key"].FirstOrDefault();

            if (apiKey == null || !IsValidApiKey(apiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool IsValidApiKey(string apiKey)
        {
            // Replace with your logic to validate the API key
            return apiKey == "ztpvzE5zFnxyXZbCoTsrrkzS5d9VNO";
        }
    }

}
