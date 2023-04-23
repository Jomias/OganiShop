using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class BlogModel : BaseModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        public string? Slug { get; set; }

        public string? Description { get; set; }

        public string? Content { get; set; }
        [Required]
        public string Image { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        [Required]
        public int CategoryBlogId { get; set; }

    }
}
