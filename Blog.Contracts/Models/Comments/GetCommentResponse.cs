using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Models.Comments
{
    public class GetCommentResponse
    {
        public CommentView Comment { get; set; }
    }

    public class CommentView
    {
        public Guid Id { get; set; }
        public DateTime AddDate { get; set; }
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
