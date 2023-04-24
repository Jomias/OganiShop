using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class BlogModel : BaseModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; } = null!;
        [MaxLength(100)]
        [DisplayName("Đường dẫn chuẩn CEO")]
        [RegularExpression("^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Chỉ có thể là kí tự, số và dấu gạch")]
        public string? Slug { get; set; }
        [DisplayName("Mô tả")]
        public string? Description { get; set; }
        [DisplayName("Nội dung Blog")]
        public string? Content { get; set; }
        [Required]
        [DisplayName("Ảnh đại diện")]
        public string Image { get; set; } = null!;
        [Required]
        [DisplayName("Danh mục Blog")] 
        public int CategoryBlogId { get; set; }

    }
}
