﻿using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public class UserServices : IUserServices
    {
        private ApplicationDbContext _context;
        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public string AddUser(User User)
        {
            try 
            {
                _context.Users.Add(User);
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

        public string DeleteUser(User User)
        {
            try
            {
                _context.Users.Attach(User);
                _context.Users.Remove(User);
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

        public string UpdateUser(User User)
        {
            try
            {
                _context.Users.Update(User);
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
