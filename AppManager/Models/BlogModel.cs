using System;

namespace AppManager.Models
{
    public class BlogModel : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int BlogAvatarId { get; set; }
        public string BlogAvatarFilePath { get; set; }
    }
}