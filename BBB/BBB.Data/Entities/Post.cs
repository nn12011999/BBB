using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBB.Data.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string Url { get; set; }
        public DateTime TimeStamp { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
