namespace BikeSparePartsShop.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid OrderId { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
