using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class ShopOrderModel : BaseModel
    {
        public int? Id { get; set; }

        public decimal? TotalPrice { get; set; }

        [Required]
        public string Account { get; set; } = null!;
        [Required]
        public int AddressId { get; set; }
    }
}
