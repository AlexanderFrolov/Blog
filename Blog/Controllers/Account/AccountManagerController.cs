using AutoMapper;
using Blog.Data.Models;
using Blog.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AccountManagerController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }


        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }


        //[HttpGet]
        //public IActionResult Login(string returnUrl = null)
        //{
        //    return View(new LoginViewModel { ReturnUrl = returnUrl });
        //}


        [Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPage()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

           // var model = new UserViewModel(result);

           // model.Friends = await GetAllFriend(model.User);

           // return View("User");
            return RedirectToAction("Index", "Home");
        }


        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = _mapper.Map<User>(model);

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        //[Route("Login")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
     
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //       // var user = _mapper.Map<User>(model);
                              

        //        var user = _userManager.FindByEmailAsync(model.Email).Result;

        //        if (user != null) 
        //        {
        //            // третий параметр для Remember me (checkbox)
        //            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        //            if (result.Succeeded)
        //            {

        //                return RedirectToAction("Index", "Home");
        //                //if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //                //{
        //                //    return Redirect(model.ReturnUrl);
        //                //}
        //                //else
        //                //{
        //                //    return RedirectToAction("Index", "Home");
        //                //}
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
        //        }
               
               
        //    }
        //    return View("Login");
        //}

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
