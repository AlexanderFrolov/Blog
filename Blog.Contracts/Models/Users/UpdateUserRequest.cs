namespace Blog.Contracts.Models.Users
{
    public class UpdateUserRequest
    {     
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }     
    }
}
