using AutoMapper;
using Blog.Contracts.Models.Users;
using Blog.Data.Models;
using Blog.Data.Queries;
using Blog.Data.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace Blog.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IMapper _mapper;
        private IUserRepository _users;

        public UsersController(
            IMapper mapper,
            IUserRepository users)
        {
            _mapper = mapper;
            _users = users;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult AllUsers()
        //{
        //    return View();
        //}

        /// <summary>
        /// view all users 
        /// </summary>
        [HttpGet]
        [Route("AllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _users.GetUsers();

            var response = new GetUsersResponse
            {
                Users = _mapper.Map<User[], UsersView[]>(users)
            };

            return View(response);
        }



        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(string email, string password)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Данные введены не корректно");

            User user = await _users.GetUserByEmail(email);

            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            // т.к. у пользователя может быть не одна роль а клайм роли принимает строку вторым аргументом
            // переделаем список ролей в строку где через запятую перечислены все роли.
            string userRoles = string.Join(",", user.Roles.Select(n => n.Name).ToArray());

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRoles)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            var response = new GetUserResponse
            {
                User = _mapper.Map<User, UserView>(user)
            };

            return StatusCode(200, response);
        }


        /// <summary>
        /// view user by id
        /// </summary>
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await _users.GetUser(id);

            if (user is null)
                return StatusCode(400, $"Ошибка! User c id:{id} не найден!");

            var response = new GetUserResponse
            {
                User = _mapper.Map<User, UserView>(user)
            };

            return StatusCode(200, response);
        }

     
        /// <summary>
        /// update user
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] Guid id,
            [FromBody] UpdateUserRequest request)
        {
            var user = await _users.GetUser(id);

            if (user is null)
                return StatusCode(400, $"Ошибка! User c id:{id} не найден!");

            await _users.UpdateUser(
               user,
               new UpdateUserQuery(
               request.Email,
               request.Password,
               request.FirstName,
               request.LastName,
               request.DisplayName
               ));

            return StatusCode(201, $"Пост {id} успешно изменен.");
        }

        /// <summary>
        /// add user
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            var user = _mapper.Map<AddUserRequest, User>(request);

            await _users.RegisterUser(user);

            return StatusCode(201, $"Пользователь: {user.FirstName} {user.LastName} успешно зарегистрирован!");
        }

        /// <summary>
        /// deleting user by id
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _users.GetUser(id);

            if (user is null)
                return StatusCode(400, $"Ошибка удаления пользователя. Пользователь c id: {id} не найден!");

            await _users.DeleteUser(user);

            return StatusCode(200, $"Пользователь {user.FirstName} {user.LastName} :: {user.Id} успешно удален!");
        }
    }
}
