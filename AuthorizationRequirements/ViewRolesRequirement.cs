using Microsoft.AspNetCore.Authorization;

namespace BikeSparePartsShop.AuthorizationRequirements
{
    public class ViewRolesRequirement : IAuthorizationRequirement
    {
        public int Months { get; }

        //The constructor takes an int as a parameter and ensures that it is NOT a positive number 
        public ViewRolesRequirement(int months)
        {
            Months = months > 0 ? 0 : months;
        }

    };

}

