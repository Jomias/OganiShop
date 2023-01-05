namespace AppManager.Models
{
    public class ProductImageModel : BaseModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FileId { get; set; }
        public string FilePath { get; set; }
        public bool IsAvatar { get; set; }
    }
}