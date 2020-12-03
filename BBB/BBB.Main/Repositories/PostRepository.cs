using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Repositories
{
    public class PostRepository : IPostRepository
    {
        private ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Post FindById(int PostId)
        {
            return _context.Posts.Find(PostId);
        }

        public Post FindByTitle(string PostTitle)
        {
            return _context.Posts.Where(x => x.Title == PostTitle).FirstOrDefault();
        }

        public IList<Post> GetAllPost()
        {
            return _context.Posts.ToList();
        }
    }
}
