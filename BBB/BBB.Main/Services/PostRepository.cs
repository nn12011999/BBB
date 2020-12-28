using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public class PostServices : IPostServices
    {
        private ApplicationDbContext _context;
        public PostServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public string AddPost(Post Post)
        {
            try 
            {
                _context.Posts.Add(Post);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string DeletePost(Post Post)
        {
            try
            {
                _context.Posts.Attach(Post);
                _context.Posts.Remove(Post);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string UpdatePost(Post Post)
        {
            try
            {
                _context.Posts.Update(Post);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
