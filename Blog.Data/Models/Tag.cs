using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Models
{
    [Table("Tags")]
    public class Tag
    {   
        public string TagId { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
