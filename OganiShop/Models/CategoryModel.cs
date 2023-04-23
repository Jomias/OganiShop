using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class CategoryModel : BaseModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string? Slug { get; set; } = null!;
        [Required]
        public string Image { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }

    }
}
