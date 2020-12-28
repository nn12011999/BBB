using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface IFileSaveRepository
    {
        public IList<FileSave> GetAll();
        public FileSave GetById(int Id);
    }
}
