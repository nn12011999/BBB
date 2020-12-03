using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        private ApplicationDbContext _context;

        public PostTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PostTag> FindByPostId(int PostId)
        {
            return _context.PostTags.Where(x=>x.PostId==PostId).ToList();
        }

        public PostTag FindByPostId_TagId(int PostId, int TagId)
        {
            return _context.PostTags.Where(x => (x.PostId == PostId&&x.TagId==TagId)).FirstOrDefault();
        }

        public IList<PostTag> FindByTagId(int TagId)
        {
            return _context.PostTags.Where(x => x.TagId == TagId).ToList();
        }

        public IList<PostTag> GetAllPostTag()
        {
            return _context.PostTags.ToList();
        }
    }
}
