using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Models;
using Blog.Data.Queries;

namespace Blog.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// delete user
        /// </summary>
        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get user by id
        /// </summary>
        public async Task<User> GetUser(Guid id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get user by email
        /// </summary>
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Include(r => r.Roles).Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get all users
        /// </summary>
        public async Task<User[]> GetUsers()
        {
            return await _context.Users.Include(r => r.Roles).ToArrayAsync();

        }

        /// <summary>
        /// add user
        /// </summary>
        public async Task RegisterUser(User user)
        {
            user.Roles = await _context.Roles.Where(r => r.Name == "User").ToListAsync();
        
            var entry = _context.Entry(user);

            if(entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update user
        /// </summary>
        public async Task UpdateUser(User user, UpdateUserQuery query)
        {
            if(!string.IsNullOrEmpty(query.Email))
                user.Email = query.Email;
            if (!string.IsNullOrEmpty(query.FirstName))
                user.FirstName = query.FirstName;
            if (!string.IsNullOrEmpty(query.LastName))
                user.LastName = query.LastName;
            if (!string.IsNullOrEmpty(query.DisplayName))
                user.DisplayName = query.DisplayName;
            if (!string.IsNullOrEmpty(query.Password))
                user.Password = query.Password;

            var entry = _context.Entry(user);
            if(entry.State == EntityState.Detached)
                _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }
    }
}
