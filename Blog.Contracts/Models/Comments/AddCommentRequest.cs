namespace Blog.Contracts.Models.Comments
{
    public class AddCommentRequest
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public Guid PostId { get; set; }   
    }
}
