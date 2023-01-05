using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Entities
{
    [Table("BlogTag")]
    public class BlogTagEntity : BaseEntity
    {
        [Key]
        public int BlogId { get; set; }
        [Key]
        public int TagId { get; set; }
    }
}