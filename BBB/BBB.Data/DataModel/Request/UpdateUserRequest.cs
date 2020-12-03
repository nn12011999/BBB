using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Request
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
