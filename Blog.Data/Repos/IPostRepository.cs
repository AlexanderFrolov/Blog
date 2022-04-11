using Blog.Data.Models;
using Blog.Data.Queries;

namespace Blog.Data.Repos
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к обьектам типа Post в базе
    /// </summary>
    public interface IPostRepository
    {
        Task SavePost(Post post, User user);
        Task DeletePost(Post post);
        Task UpdatePost(Post post, UpdatePostQuery query);
        Task<Post[]> GetPosts();
        Task<Post[]> GetPostsByUserId(Guid userId);
    }
}
