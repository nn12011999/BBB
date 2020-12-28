using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Request
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
