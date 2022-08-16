using Blog.Models;
using Blog.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Controllers
{
    //[Route("[controller]")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            return View(new MainViewModel());
        }

        [Route("[action]")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet]
        //[Route("index")]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View(); 
        //}

        //[HttpGet]
        //[Route("authorization")]
        //public IActionResult SignIn()
        //{

        //    return View();
        //}


        //[HttpPost]
        //[Route("authorization")]
        //public IActionResult SignIn(LoginViewModel model)
        //{

        //    if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        //        return BadRequest("Email и/или пароль не установлены");

        //    string email = model.Email;
        //    string password = model.Password;
        //    return Redirect(model.ReturnUrl ?? "/Home/index");          
        //}


        //[HttpGet]
        //[Route("register")]
        //public IActionResult RegisterUser()
        //{
        //    return View();
        //}




    }
}