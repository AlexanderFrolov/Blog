using System.ComponentModel.DataAnnotations.Schema;
using SQLite;
using SQLiteNetExtensions.Attributes;
using TableAttribute = SQLite.TableAttribute;

namespace Blog.Data.Models
{
    [Table("Posts")]
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Contetnt { get; set; }
      
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
