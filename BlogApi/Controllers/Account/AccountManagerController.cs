using AutoMapper;
using Blog.Contracts.Models.Users;
using Blog.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers.Account
{
    [ApiController]
    [Route("[controller]")]
    public class AccountManagerController : ControllerBase
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountManagerController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthorizeUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(request);

                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, request.RememberMe, false);

                if (result.Succeeded)
                {
                    return StatusCode(200, $"Пользователь {request.UserName} авторизован");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    return BadRequest(ModelState);
                }
            }
            return StatusCode(400);
        }


        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return StatusCode(200, "Пользователь успешно разлогинился.");
        }

        [Authorize]
        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            return StatusCode(200, result);
        }


        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {      
            if (User is null)
                return StatusCode(400, $"Ошибка! Вы не авторизованы!");

            var user = await _userManager.GetUserAsync(User);
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);

            return StatusCode(201, $"Пользователь {user.FirstName} {user.LastName} :: {user.Id} успешно удален!");
        }


        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            if (User is null)
                return StatusCode(400, $"Ошибка! Вы не авторизованы!");

            if (ModelState.ErrorCount != 0) return BadRequest(ModelState);
         
            var userModel = _mapper.Map<User>(request);

            var user = await _userManager.GetUserAsync(User);

            if (request.FirstName != null) user.FirstName = request.FirstName;
            if (request.LastName != null) user.LastName = request.LastName;
            if (request.Email != null) user.Email = request.Email;
           
            var result = await _userManager.UpdateAsync(user);

            return StatusCode(201, $"Пользователь {user.FirstName} :: {user.Id} успешно изменен.");
        }

    }
}
