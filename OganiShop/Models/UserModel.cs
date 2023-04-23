using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class UserModel : BaseModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Độ dài không được vượt quá 30 ký tự")]
        public string Account { get; set; } = null!;
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Độ dài không được vượt quá 30 ký tự")]
        public string Password { get; set; } = null!;
        public string? Role { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Độ dài không được vượt quá 30 ký tự")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Độ dài không được vượt quá 30 ký tự")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [DisplayName("Ảnh đại diện")]
        public string? Image { get; set; }
        [DisplayName("Ảnh đại diện")]
        [Required]
        public IFormFile? ImageFile { get; set; }
    }
}
