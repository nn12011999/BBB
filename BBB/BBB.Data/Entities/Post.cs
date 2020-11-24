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
        public  int UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string title { get; set; }
        public string context { get; set; }
        public string Url { get; set; }
        public DateTime TimeStamp { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
