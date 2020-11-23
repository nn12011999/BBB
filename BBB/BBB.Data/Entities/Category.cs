using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBB.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("ParentId")]
        public string ParentId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public string slug { get; set; }
    }
}
