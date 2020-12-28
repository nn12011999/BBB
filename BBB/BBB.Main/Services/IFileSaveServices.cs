using BBB.Data.Entities;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public interface IFileSaveServices
    {
        public Task<string> AddFileSave(FileSave fileSave);
        public string DeleteFileSave(FileSave fileSave);
        public Task<string> UpdateCategory(FileSave fileSave);
    }
}
