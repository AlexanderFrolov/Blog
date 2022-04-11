using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
