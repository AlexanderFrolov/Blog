namespace Blog.Contracts.Models.Tags
{
    public class GetTagResponse
    {
        public TagView Tag { get; set; }
    }

    public class TagView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
