using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Models;
using Blog.Data.Queries;
using System.Collections;

namespace Blog.Data.Repos
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// delete post
        /// </summary>
        public async Task DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();  
        }

        /// <summary>
        /// get post by id
        /// </summary>
        public async Task<Post> GetPostById(Guid id)
        {
            return await _context.Posts.Include(t => t.Tags).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get all posts
        /// </summary>
        public async Task<Post[]> GetAllPosts()
        {
            return await _context.Posts.Include(t => t.Tags).ToArrayAsync();
        }

        /// <summary>
        /// get Posts by userId
        /// </summary>
        public async  Task<Post[]> GetPostsByUserId(Guid userId)
        {           
            return await _context.Posts
                .Include(u => u.User)
                .Where(u => u.User.Id == userId)
                .ToArrayAsync();
        }

        /// <summary>
        /// add new Post
        /// </summary>
        public async Task SavePost(Post post, User user, List<Tag> tags)
        {                    
            post.User = user;
            post.Tags = tags;

            var entry = _context.Entry(post);

            if (entry.State == EntityState.Detached)
                await _context.Posts.AddAsync(post);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update post
        /// </summary>
        public async Task UpdatePost(Post post, UpdatePostQuery query)
        {        
            if (!string.IsNullOrEmpty(query.Title))
                post.Title = query.Title;
            if (!string.IsNullOrEmpty(query.Contetnt))
                post.Content = query.Contetnt;
            if (!string.IsNullOrEmpty(query.ShortDescription))
                post.ShortDescription = query.ShortDescription;
            if (query.Tags is not null)
                post.Tags = query.Tags;
         
            var entry = _context.Entry(post);
            if (entry.State == EntityState.Detached)
                _context.Posts.Update(post);
 
            await _context.SaveChangesAsync();
        }
    }
}
