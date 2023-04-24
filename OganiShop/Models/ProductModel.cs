using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class ProductModel : BaseModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string? Slug { get; set; } = null!;
        [DisplayName("Đơn giá")]
        public decimal? Price { get; set; }
        [DisplayName("Giảm giá")]
        public int? Discount { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; }
        [DisplayName("Tóm lược")]
        public string? Summary { get; set; }
        [DisplayName("Khối lượng")]
        public double? Weight { get; set; }

        [DisplayName("Đơn vị tính")]
        public string? Unit { get; set; }
        [DisplayName("Ảnh đại diện")] 
        public string? Image { get; set; } = null!;
        [DisplayName("Số lượng trong kho")]
        public int? Quantity { get; set; }
        [Required]
        [DisplayName("Danh mục sản phẩm")]
        public int CategoryId { get; set; }
    }
}
