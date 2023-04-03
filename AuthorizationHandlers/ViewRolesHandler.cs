using Microsoft.AspNetCore.Authorization;
using BikeSparePartsShop.AuthorizationRequirements;

namespace BikeSparePartsShop.AuthorizationHandlers
{
    public class ViewRolesHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            // The PendingRequirements of the current user return all unsatisfied requirements. They need to be passed to a list so that we can execute operations against them. 
            foreach (var requirement in context.PendingRequirements.ToList())
            {
                //We use pattern matching to identify ViewRolesRequirements in the original class and assign them to a local variable  req
                if (requirement is ViewRolesRequirement req)
                {
                    var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
                    if (joiningDateClaim == null) //no joining date
                    {
                        return Task.CompletedTask;
                    }
                    var joiningDate = Convert.ToDateTime(joiningDateClaim); //convert it to a datetime

                    if (joiningDate < DateTime.Now.AddMonths(req.Months))
                    {
                        Console.Write(" HasRolesHandler date " + joiningDate);
                        Console.Write("HasRolesHandler Permission " + context.User.HasClaim("Permission", "View Roles"));
                        if (context.User.HasClaim(" Permission ", " View Roles "))
                        {

                            context.Succeed((IAuthorizationRequirement)req); //they have the permissions
                        }

                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}

