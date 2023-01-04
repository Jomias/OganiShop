using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AppManager.Entities
{
    [Table("ContactMessage")]
    public class ContactMessageEntity
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
