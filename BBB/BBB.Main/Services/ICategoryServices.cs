using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public interface ICategoryServices
    {
        public string AddCategory(Category category);
        public string DeleteCategory(Category category);
        public string UpdateCategory(Category category);
    }
}
