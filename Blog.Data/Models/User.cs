using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Models
{
    [Table("Users")]
    public class User
    {
        public Guid Id  { get; set; } = Guid.NewGuid();
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string Email { get; set; }    
        public string Password { get; set; }   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
