using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BikeSparePartsShop.Models;

namespace BikeSparePartsShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BikeSparePartsShop.Models.Customer> Customer { get; set; }
        public DbSet<BikeSparePartsShop.Models.Order> Order { get; set; }
        public DbSet<BikeSparePartsShop.Models.Staff> Staff { get; set; }
        public DbSet<BikeSparePartsShop.Models.Stock> Stock { get; set; }
    }
}