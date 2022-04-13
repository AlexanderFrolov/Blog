using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Queries
{
    public class UpdatePostQuery
    {
        public string Title { get; }
        public string ShortDescription { get; }
        public string Contetnt { get; }
        public List<Tag> Tags { get; }


        public UpdatePostQuery(string title = null, string shortDescription = null, string content = null, List<Tag> newTags = null)
        {
            Title = title;
            ShortDescription = shortDescription;
            Contetnt = content;
            Tags = newTags;
        }
    }
}
