﻿using BBB.Data;
using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Repositories
{
    public class TagRepository : ITagRepository
    {
        private ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Tag FindById(int categoryId)
        {
            return _context.Tags.Find(categoryId);
        }

        public Tag FindByName(string categoryName)
        {
            return _context.Tags.Where(x => x.Name == categoryName).FirstOrDefault() ;
        }

        public IList<Tag> GetAllTag()
        {
            return _context.Tags.ToList();
        }
    }
}
