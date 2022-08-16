using AutoMapper;
using Blog.Contracts.Models.Users;
using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers.Account
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegisterController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
 
       
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AddUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(request);

                var result = await _userManager.CreateAsync(user, request.PasswordReg);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return StatusCode(201, $"Пользователь {request.FirstName} успешно зарегистрирован"); 
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        return BadRequest(ModelState);
                    }
                }
            }
            return StatusCode(400);
        }
    }
}
