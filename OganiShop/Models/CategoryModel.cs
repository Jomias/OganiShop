using AutoMapper.Configuration.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OganiShop.Models
{
    public class CategoryModel : BaseModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Tên danh mục")]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        [DisplayName("Đường dẫn chuẩn CEO")]
        [RegularExpression("^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Chỉ có thể là kí tự, số và dấu gạch")]
        public string? Slug { get; set; }
        [Required]
        [DisplayName("Ảnh đại diện")]
        public string Image { get; set; } = null!;

    }
}
