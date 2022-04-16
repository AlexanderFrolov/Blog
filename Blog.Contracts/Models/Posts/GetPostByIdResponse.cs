using Blog.Contracts.Models.Comments;
using Blog.Contracts.Models.Tags;
using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Models.Posts
{
    public class GetPostByIdResponse
    {
        public GetPostByIdView Post { get; set; }
    }

    public class GetPostByIdView
    {
        public Guid Id { get; set; }
        public DateTime AddDate { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }

        public ICollection<TagView> Tags { get; set; }
        public ICollection<CommentView> Comments { get; set; }

        public Guid UserId { get; set; }
    }
}
