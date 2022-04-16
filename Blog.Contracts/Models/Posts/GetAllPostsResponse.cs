using Blog.Contracts.Models.Tags;
using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Models.Posts
{
    public class GetAllPostsResponse
    {
        public int PostsAmount { get; set; }
        public AllPostsView[] Posts { get; set; }
    }
    
    public class AllPostsView
    {
        public Guid Id { get; set; }
        public DateTime AddDate { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }

        public ICollection<TagsView> Tags { get; set; }

        public Guid UserId { get; set; }
    }



}
