using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBB.Data.Entities
{
    public class Post
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public string title { get; set; }
        public string context { get; set; }
        public string Url { get; set; }
        public DateTime TimeStamp { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
