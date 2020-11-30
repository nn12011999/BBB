using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public interface ITagServices
    {
        public string AddTag(Tag tag);
        public string DeleteTag(Tag tag);
        public string UpdateTag(Tag tag);
    }
}
