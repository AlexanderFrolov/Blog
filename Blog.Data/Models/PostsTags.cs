namespace Blog.Data.Models
{
    public class PostsTags
    {      
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }

        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }    
}
