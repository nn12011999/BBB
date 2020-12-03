using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public class PostTagServices : IPostTagServices
    {
        private ApplicationDbContext _context;
        public PostTagServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public string AddPostTag(PostTag PostTag)
        {
            try 
            {
                _context.PostTags.Add(PostTag);
                var respone = _context.SaveChanges();
                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string DeletePostTag(PostTag PostTag)
        {
            try
            {
                _context.PostTags.Attach(PostTag);
                _context.PostTags.Remove(PostTag);
                var respone = _context.SaveChanges();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string UpdatePostTag(PostTag PostTag)
        {
            try
            {
                _context.PostTags.Update(PostTag);
                var respone = _context.SaveChanges();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
