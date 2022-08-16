using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Models
{
    [Table("Roles")]
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
