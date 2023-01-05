using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Entities
{
    [Table("BlogImage")]
    public class BlogImageEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int FileId { get; set; }
        public bool IsAvatar { get; set; }
    }
}