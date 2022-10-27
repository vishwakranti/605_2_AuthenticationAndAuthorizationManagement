using System.ComponentModel.DataAnnotations;

namespace BikeSparePartsShop.Models
{
    public class Staff
    {
        public Guid StaffId { get; set; }
        [Display(Name = "Staff Name")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
