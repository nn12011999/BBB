using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category FindById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public Category FindByName(string categoryName)
        {
            return _context.Categories.Where(x => x.Name == categoryName).FirstOrDefault() ;
        }

        public IList<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        public Category FindByUrl(string url)
        {
            return _context.Categories.Where(x => x.Slug == url).FirstOrDefault();
        }
    }
}
