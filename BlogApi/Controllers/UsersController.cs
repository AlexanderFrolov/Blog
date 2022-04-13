using AutoMapper;
using Blog.Contracts.Models.Users;
using Blog.Data.Models;
using Blog.Data.Queries;
using Blog.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
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

        /// <summary>
        /// view user by id
        /// </summary>
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
        /// view all users 
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _users.GetUsers();

            var response = new GetUsersResponse
            {
                Users = _mapper.Map<User[], UsersView[]>(users)
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
