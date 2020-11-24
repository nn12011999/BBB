using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.Entities
{
    public class PostTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
