using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class OrderDetailModel : BaseModel
    {
        public int? Id { get; set; }
        [Required]
        public int ShopOrderId { get; set; }
        [Required]
        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }
    }
}
