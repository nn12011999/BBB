using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.Entities
{
    public class PostTag
    {
        public int PostId { get; set; }
        public virtual Post post { get; set; }

        public int TagId { get; set; }
        public virtual Tag tag { get; set; }
    }
}
