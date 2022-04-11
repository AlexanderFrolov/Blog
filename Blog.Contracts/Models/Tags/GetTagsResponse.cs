namespace Blog.Contracts.Models.Tags
{
    public class GetTagsResponse
    {
        public int TagsAmount { get; set; }
        public TagsView[] Tags { get; set; }
    }

    public class TagsView
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
    }
}
