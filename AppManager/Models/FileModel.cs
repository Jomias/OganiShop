namespace AppManager.Models
{
    public class FileModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
    }
}
