using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface ITagRepository
    {
        public IList<Tag> GetAllTag();
        public Tag FindByName(string tagName);
        public Tag FindByUrl(string url);
        public Tag FindById(int tagId);
    }
}
