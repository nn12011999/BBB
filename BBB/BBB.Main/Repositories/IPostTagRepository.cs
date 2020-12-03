using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface IPostTagRepository
    {
        public IList<PostTag> GetAllPostTag();
        public IList<PostTag> FindByTagId(int TagId);
        public IList<PostTag> FindByPostId(int PostId);
        public PostTag FindByPostId_TagId(int PostId, int TagId);
    }
}
