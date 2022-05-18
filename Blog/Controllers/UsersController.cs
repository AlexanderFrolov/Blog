using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
