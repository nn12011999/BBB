using BBB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Services
{
    public interface IPostServices
    {
        public string AddPost(Post Post);
        public string DeletePost(Post Post);
        public string UpdatePost(Post Post, List<Tag> tags);

    }
}
