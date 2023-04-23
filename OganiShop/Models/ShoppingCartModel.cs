using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class ShoppingCartModel : BaseModel
    {
        public int? Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public string? Customer { get; set; }
    }
}
