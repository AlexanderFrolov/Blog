using AutoMapper;
using Blog.Contracts.Models.Roles;
using Blog.Data.Models;
using Blog.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("[controller]")]
    public class RolesController : Controller
    {
        private IRoleRepository _roles;
        private IMapper _mapper;

        public RolesController(
            IRoleRepository tags,
            IMapper mapper
            )
        {
            _roles = tags;
            _mapper = mapper;
        }


        /// <summary>
        /// view list Roles
        /// </summary>
        [HttpGet]
        [Route("Roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roles.GetAllRoles();

            var response = new GetRolesResponse
            {              
                RolesAmount = roles.Length,
                Roles = _mapper.Map<Role[], RoleView[]>(roles)
            };

            return View(response);
        }

  
    }
}
