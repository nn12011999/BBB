using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Request
{
    public class DeletePostTagRequest
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
