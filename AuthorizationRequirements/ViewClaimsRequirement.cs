using Microsoft.AspNetCore.Authorization;

namespace BikeSparePartsShop.AuthorizationRequirements
{
    public class ViewClaimsRequirement : IAuthorizationRequirement
    {
        public int Months { get; }

        //The constructor takes an int as a parameter and ensures that it is NOT a positive number 
        public ViewClaimsRequirement(int months)
        {
            Months = months > 0 ? 0 : months;
        }

    }
}
