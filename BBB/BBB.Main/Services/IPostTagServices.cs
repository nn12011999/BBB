using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public interface IPostTagServices
    {
        public string AddPostTag(PostTag PostTag);
        public string DeletePostTag(PostTag PostTag);
        public string UpdatePostTag(PostTag PostTag);
    }
}
