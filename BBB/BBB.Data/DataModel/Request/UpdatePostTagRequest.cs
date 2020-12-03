using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Request
{
    public class UpdatePostTagRequest
    {
        public int OldPostId{get; set;}
        public int OldTagId { get; set; }
        public int NewPostId { get; set; }
        public int NewTagId { get; set; }
    }
}
