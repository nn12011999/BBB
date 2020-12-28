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
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                category.Slug = category.Name.Replace(" ", "-") + "-" + category.Id;
                _context.Categories.Update(category);
                response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
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
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
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
                category.Slug = category.Name.Replace(" ", "-") + "-" + category.Id;
                _context.Categories.Update(category);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
