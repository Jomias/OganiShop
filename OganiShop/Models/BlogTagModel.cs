using System.ComponentModel.DataAnnotations;

namespace OganiShop.Models
{
    public class BlogTagModel : BaseModel
    {
        public int? Id { get; set; }
        [Required]
        public int BlogId { get; set; }
        [Required]
        public int TagId { get; set; }

    }
}
