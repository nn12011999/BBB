using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Response
{
    public class AuthenticateRessponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
