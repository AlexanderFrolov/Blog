using Microsoft.AspNetCore.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
