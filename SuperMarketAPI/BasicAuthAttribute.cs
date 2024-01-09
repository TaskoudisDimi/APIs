using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace SuperMarketAPI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BasicAuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                // Extract credentials from Authorization header
                string encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                byte[] decodedBytes = Convert.FromBase64String(encodedUsernamePassword);
                string decodedCredentials = Encoding.UTF8.GetString(decodedBytes);
                string[] credentials = decodedCredentials.Split(':', 2);

                // Check your credentials logic here (e.g., verify against database)
                string validUsername = "yourUsername";
                string validPassword = "yourPassword";

                if (credentials.Length == 2 && credentials[0] == validUsername && credentials[1] == validPassword)
                {
                    // Authorization successful
                    return;
                }
            }

            // Authorization failed
            context.Result = new UnauthorizedResult();
        }
    }
}
