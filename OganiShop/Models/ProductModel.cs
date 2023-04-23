using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class ProductModel : BaseModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Slug { get; set; } = null!;

        public decimal? Price { get; set; }

        public int? Discount { get; set; }

        public string? Description { get; set; }

        public string? Summary { get; set; }

        public double? Weight { get; set; }

        public string? Unit { get; set; }

        [Required] 
        public string Image { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        public int? Quantity { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
