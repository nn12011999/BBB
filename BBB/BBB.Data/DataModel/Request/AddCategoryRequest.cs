using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Request
{
    public class AddCategoryRequest
    {
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
        public string Slug { get; set; }
    }
}
