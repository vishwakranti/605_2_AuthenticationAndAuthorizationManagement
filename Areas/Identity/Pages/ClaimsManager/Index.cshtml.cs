using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeSparePartsShop.ClaimsManager
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        //import the userManger and generate a list of users
        public UserManager<IdentityUser> _userManager { get; set; }
        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public List<IdentityUser> Users { get; set; }
        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
        }

    }
    
        
    
}
