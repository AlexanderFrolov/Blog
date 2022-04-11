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
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Contetnt { get; set; }
        public ICollection<Tag> Tags { get; set; }


        public UpdatePostQuery(string title = null, string shortDescription = null, string content = null, List<Tag> tags = null)
        {
            Title = title;
            ShortDescription = shortDescription;
            Contetnt = content;
            Tags = tags;
        }
    }
}
