using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Request
{
    public class UpdateTagRequest
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string Url { get; set; }
    }
}
