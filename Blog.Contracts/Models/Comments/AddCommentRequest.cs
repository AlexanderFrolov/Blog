namespace Blog.Contracts.Models.Comments
{
    public class AddCommentRequest
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }   
    }
}
