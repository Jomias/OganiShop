using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class TagModel : BaseModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Tên Tag")]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        [DisplayName("Đường dẫn chuẩn CEO")]
        [RegularExpression("^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Chỉ có thể là kí tự, số và dấu gạch")]
        public string? Slug { get; set; } = null!;

    }
}
