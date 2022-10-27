﻿using System.ComponentModel.DataAnnotations;

namespace BikeSparePartsShop.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Shipped Date")]
        public DateTime ShippedDate { get; set; }
        [Display(Name = "Customer")]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [Display(Name = "Stock")]
        public Guid StockId { get; set; }
        public Stock? Stock { get; set; }
        [Display(Name = "Staff")]
        public Guid StaffId { get; set; }
        public Staff? Staff { get; set; }
    }
}
