using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public class TagServices : ITagServices
    {
        private ApplicationDbContext _context;
        public TagServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public string AddTag(Tag tag)
        {
            try 
            {
                _context.Tags.Add(tag);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                tag.Url = tag.Name.Replace(" ", "-") + "-" + tag.Id;
                _context.Tags.Update(tag);
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

        public string DeleteTag(Tag tag)
        {
            try
            {
                _context.Tags.Attach(tag);
                _context.Tags.Remove(tag);
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

        public string UpdateTag(Tag tag)
        {
            try
            {
                tag.Url = tag.Name.Replace(" ", "-") + "-" + tag.Id;
                _context.Tags.Update(tag);
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
