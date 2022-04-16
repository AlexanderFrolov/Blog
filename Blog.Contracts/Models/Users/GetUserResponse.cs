using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Models.Users
{
    public class GetUserResponse
    {
        public UserView User { get; set; }
    }

    public class UserView
    {
        public Guid Id { get; set; }
        public DateTime AddDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        //public ICollection<Post> Posts { get; set; }

        //public ICollection<Comment> Comments { get; set; }
    }

}
