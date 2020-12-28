using BBB.Data;
using BBB.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Repositories
{
    public class FileSaveRepository : IFileSaveRepository
    {
        private ApplicationDbContext _context;
        public FileSaveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FileSave> GetAll()
        {
            return _context.FileSaves.ToList();
        }

        public FileSave GetById(int Id)
        {
            return  _context.FileSaves.Find(Id);
        }
        public FileSave GetByUrl(string url)
        {
            return _context.FileSaves.Where(x=> x.Url == url).FirstOrDefault();
        }
    }
}
