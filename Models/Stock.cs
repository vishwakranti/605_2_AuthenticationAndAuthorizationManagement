using System.ComponentModel.DataAnnotations;

namespace BikeSparePartsShop.Models
{
    public class Stock
    {
        public Guid StockId { get; set; }
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
