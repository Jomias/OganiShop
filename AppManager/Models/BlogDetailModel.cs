using System;
using System.Collections.Generic;

namespace AppManager.Models
{
    public class BlogDetailModel : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorRole { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int AvatarId { get; set; }
        public string Avatar { get; set; }
        public List<string> ListTags { get; set; }
        public string Account { get; set; }
    }
}
