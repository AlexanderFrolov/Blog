using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Models.Posts
{
    public class AddPostRequest
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }

        public List<Guid> TagsId { get; set; }
        public Guid UserId { get; set; }
    }
}
