using Blog.Data.Models;

namespace Blog.Contracts.Models.Posts
{
    public class UpdatePostRequest
    { 
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }

        public List<Guid> TagsId { get; set; }   
    }
}
