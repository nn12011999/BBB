using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User FindById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User FindByName(string userName)
        {
            return _context.Users.Where(x => x.UserName == userName).FirstOrDefault() ;
        }

        public IList<User> GetAllUser()
        {
            return _context.Users.ToList();
        }
    }
}
