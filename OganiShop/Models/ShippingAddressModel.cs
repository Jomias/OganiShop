using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class ShippingAddressModel
    {
        public int? Id { get; set; }
        public string? Account { get; set; } = null!;
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Độ dài tên không được vượt quá 50 ký tự")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Độ dài tên không được vượt quá 50 ký tự")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Độ dài địa chỉ không được vượt quá 1000 ký tự")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Thành phố không được để trống")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "Quốc gia không được để trống")]
        public string Country { get; set; } = null!;
        [Required(ErrorMessage = "Post Code không được để trống")]
        public string PostCode { get; set; } = null!;
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        //[RegularExpression(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/", ErrorMessage = "Sai định dạng số điện thoại")]
        public string Phone { get; set; } = null!;
        [StringLength(200, MinimumLength = 0, ErrorMessage = "Độ dài không được vượt quá 200 ký tự")]
        public string? Note { get; set; }

    }
}