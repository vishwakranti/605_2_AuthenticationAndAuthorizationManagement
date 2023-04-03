using BikeSparePartsShop.Data;
using BikeSparePartsShop.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BikeSparePartsShop.Areas.Identity.Pages.RolesManager
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;

        public List<IdentityRole> Roles { get; set; }

        public List<UserRoles> UserAndRoles { get; set; }

        //create the Users and Roles from the DB
        public List<UserRoles> GetUserAndRoles()
        {
            var list = (from user in _context.Users
                        join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                        join role in _context.Roles on userRoles.RoleId equals role.Id
                        select new UserRoles { UserName = user.UserName, RoleName = role.Name }).ToList();
            return list;
        }
        public IndexModel(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public void OnGet()
        {
            //pass the Roles to the front end
            Roles = _roleManager.Roles.ToList();
            //Pass the users and roles to the front end
            UserAndRoles = GetUserAndRoles();
        }
    }
}
