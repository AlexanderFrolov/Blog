using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Models
{
    //[Table("Tags")]
    public class Tag
    {
        //public Guid Id { get; set; } = Guid.NewGuid();
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        //public ICollection<Post> Posts { get; set; }

      //  public Guid UserId { get; set; }
       // public User User { get; set; }
    }
}
