using System.ComponentModel.DataAnnotations;

namespace BBB.Data.Entities
{
    public class FileSave
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string FileType { get; set; }
    }
}
