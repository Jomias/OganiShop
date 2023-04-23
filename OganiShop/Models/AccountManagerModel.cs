using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class AccountManagerModel
    {
        [Required(ErrorMessage = "Chưa điền tên đăng nhập")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Độ dài không được vượt quá 30 ký tự")]
        public string Account { get; set; } = null!;
        [Required(ErrorMessage = "Chưa điền mật khẩu")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Độ dài không được vượt quá 30 ký tự")]
        public string Password { get; set; } = null!;
        public string? Role { get; set; }
        public string? ReturnUrl { get; set; }

    }
}