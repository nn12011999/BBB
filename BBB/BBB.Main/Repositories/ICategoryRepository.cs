using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface ICategoryRepository
    {
        public IList<Category> GetAllCategory();
        public Category FindByName(string categoryName);
        public Category FindByUrl(string url);
        public Category FindById(int categoryId);
    }
}
