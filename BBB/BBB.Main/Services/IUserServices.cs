using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public interface IUserServices
    {
        public string AddUser(User user);
        public string DeleteUser(User user);
        public string UpdateUser(User user);
    }
}
