using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Логин", Prompt = "Введите логин")]
        public string UserName { get; set; }

        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email", Prompt = "Введите email")]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

       // public string ReturnUrl { get; set; }

    }
}
