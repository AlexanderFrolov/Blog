using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Models
{
    public class User : IdentityUser
    {    
        public DateTime AddDate { get; set; } = DateTime.Now;        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Guid Id  { get; set; } = Guid.NewGuid();
        // public string Email { get; set; }    
        // public string Password { get; set; } 
        // public DateTime BirthDate { get; set; }
        //public string UserName { get; set; }

        // public ICollection<Post> Posts { get; set; }
        // public ICollection<Comment> Comments { get; set; }

        // public ICollection<Tag> Tags { get; set; }

        // public ICollection<Role> Roles { get; set; }
    }
}
