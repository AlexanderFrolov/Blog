using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Queries
{
    public class UpdateUserQuery
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        public UpdateUserQuery(
            string email = null,
            string password = null,
            string firstName = null,
            string lastName = null,
            string displayName = null)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;    
            DisplayName = displayName;  
        }
    }
}
