using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBB.Data.Entities
{
    public class Tag
    {

        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
