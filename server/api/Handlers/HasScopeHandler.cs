
using Microsoft.AspNetCore.Authorization;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasScopeRequirement requirement)
        {
            var permissions = context.User.FindAll(c => c.Type == "permissions").ToArray();

            if (context.User.HasClaim(c => c.Type == "scope" &&
                c.Issuer == requirement.Issuer &&
                permissions.Any(s => s.Value == requirement.Scope)))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }