using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface ITagRepository
    {
        public IList<Tag> GetAllTag();
        public Tag FindByName(string categoryName);
        public Tag FindById(int categoryId);
    }
}
