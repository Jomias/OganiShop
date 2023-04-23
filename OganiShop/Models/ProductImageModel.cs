using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class ProductImageModel : BaseModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        public string Path { get; set; } = null!;
        public IFormFile? PathFile { get; set; }
        public bool? IsAvatar { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
