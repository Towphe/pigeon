
using data.account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Filters
{
    public class Auth0IPFilter : Attribute, IAsyncAuthorizationFilter
    {
        public Auth0IPFilter()
        {
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // check if ip address is valid
            if (!Auth0Constants.ValidIPs.Contains(context.HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}