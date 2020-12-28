using BBB.Data;
using BBB.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        public Post FindByUrl(string url)
        {
            return _context.Posts.Where(x => x.Url == url).FirstOrDefault();
        }

        public IList<Post> GetAllPost()
        {
            var query = _context.Posts.Include(x=>x.Tags).ToList();
            foreach (Post post in query)
            {
                post.Tags = null;
            }
            return query;
        }
    }
}
