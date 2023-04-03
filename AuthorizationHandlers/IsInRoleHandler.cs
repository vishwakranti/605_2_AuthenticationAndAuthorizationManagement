using Microsoft.AspNetCore.Authorization;
using BikeSparePartsShop.AuthorizationRequirements;

namespace BikeSparePartsShop.AuthorizationHandlers
{
    public class IsInRoleHandler : AuthorizationHandler<ViewRolesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
           ViewRolesRequirement req)
        {
            if (context.User.IsInRole("Admin")) //does the user have the role Admin?
            {
                context.Succeed((IAuthorizationRequirement)req);
            }
            return Task.CompletedTask;
        }
    }
}
