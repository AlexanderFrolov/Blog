using Microsoft.EntityFrameworkCore;
using Blog.Data.Models;

namespace Blog.Data.Repos
{
    public class RoleRepository //: IRoleRepository
    {
        private readonly BlogContext _context;

        public RoleRepository(BlogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// delete Role
        /// </summary>
        public async Task DeleteRole(Role role)
        {
           // _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get all Roles
        /// </summary>
      //  public async Task<Role[]> GetAllRoles()
      //  {
           // return await _context.Roles.ToArrayAsync();
      //  }

        /// <summary>
        /// add new Role
        /// </summary>
        public async Task SaveRole(Role role)
        {            
            var entry = _context.Entry(role);

            if (entry.State == EntityState.Detached)
              //  await _context.Roles.AddAsync(role);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update Role
        /// </summary>
        public async Task UpdateRole(Role role, string newName, string newDescription)
        {
            if (!string.IsNullOrEmpty(newName))
                role.Name = newName;
            if (!string.IsNullOrEmpty(newDescription))
                role.Description = newDescription;
      
            var entry = _context.Entry(role);
            if (entry.State == EntityState.Detached)
              //  _context.Roles.Update(role);

            await _context.SaveChangesAsync();
        }
    }
}
