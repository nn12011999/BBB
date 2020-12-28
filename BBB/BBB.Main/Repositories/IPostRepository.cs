using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface IPostRepository
    {
        public IList<Post> GetAllPost();
        public Post FindByTitle(string PostTitle);
        public Post FindByUrl(string url);
        public Post FindById(int PostId);
    }
}
