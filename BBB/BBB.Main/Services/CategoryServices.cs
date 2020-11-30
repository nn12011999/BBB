using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public class CategoryServices : ICategoryServices
    {
        private ApplicationDbContext _context;
        public CategoryServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public string AddCategory(Category category)
        {
            try 
            {
                _context.Categories.Add(category);
                var respone = _context.SaveChanges();
                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string DeleteCategory(Category category)
        {
            try
            {
                _context.Categories.Attach(category);
                _context.Categories.Remove(category);
                var respone = _context.SaveChanges();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string UpdateCategory(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                var respone = _context.SaveChanges();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
