using System;

namespace AppManager.Models
{
    public class BaseModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int? Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
