namespace Blog.Contracts.Models.Tags
{
    public class AddTagRequest
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
