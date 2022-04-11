using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Models.Tags
{
    public class GetTagsResponse
    {
        public int TagsAmount { get; set; }
        public TagsView[] Tags { get; set; }
    }

    public class TagsView
    {
        public string TagId { get; set; }
    }
}
