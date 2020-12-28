using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBB.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = RoleDefine.User;
    }

    public static class RoleDefine
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string UserAndAdmin = "User,Admin";
    }
}
