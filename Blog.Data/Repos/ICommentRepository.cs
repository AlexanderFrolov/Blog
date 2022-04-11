using Blog.Data.Models;
using Blog.Data.Queries;

namespace Blog.Data.Repos
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к обьектам типа Comment в базе
    /// </summary>
    public interface ICommentRepository
    {
        Task SaveComment(Comment comment, User user, Post post);
        Task DeleteComment(Comment comment);
        Task UpdateComment(Comment comment, string newContent);
        Task<Comment> GetComment(Guid id);
        Task<Comment[]> GetComments();
        //Task<Comment[]> GetCommentsByPostId(Guid postId);
    }
}
