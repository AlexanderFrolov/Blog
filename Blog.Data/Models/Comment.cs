using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Models
{
    [Table("Comments")] 
    public class Comment
    {
       
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
