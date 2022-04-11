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
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public CommentRepository(BlogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// add comment
        /// </summary>
        public async Task SaveComment(Comment comment, User user, Post post)
        {
            comment.Post = post;
            comment.PostId = post.Id;

            comment.User = user;
            comment.UserId = user.Id;

            var entry = _context.Entry(comment);

            if (entry.State == EntityState.Detached)
                await _context.Comments.AddAsync(comment);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete comment
        /// </summary>
        public async Task DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get comment by id
        /// </summary>
        public async Task<Comment> GetComment(Guid id)
        {
            return await _context.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get all comments
        /// </summary>
        public async Task<Comment[]> GetComments()
        {
            return await _context.Comments.ToArrayAsync();
        }

        /// <summary>
        /// update comment
        /// </summary>
        public async Task UpdateComment(Comment comment, string newContent)
        {
            if (!string.IsNullOrEmpty(newContent))
                comment.Content = newContent;

            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
                _context.Comments.Update(comment);

            await _context.SaveChangesAsync();
        }
    }
}
