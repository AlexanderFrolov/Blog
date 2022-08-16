using System.ComponentModel.DataAnnotations;

namespace Blog.Contracts.Models.Users
{
    public class AuthorizeUserRequest
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Логин", Prompt = "Введите логин")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
