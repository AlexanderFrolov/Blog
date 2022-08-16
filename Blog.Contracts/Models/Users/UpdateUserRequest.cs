using System.ComponentModel.DataAnnotations;

namespace Blog.Contracts.Models.Users
{
    public class UpdateUserRequest
    {       
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string FirstName { get; set; }
      
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string LastName { get; set; }
        
        [EmailAddress]
        [Display(Name = "Email", Prompt = "example@gmail.com")]
        public string Email { get; set; }       
    }
}
