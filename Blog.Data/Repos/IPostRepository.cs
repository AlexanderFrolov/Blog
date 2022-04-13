using Blog.Data.Models;
using Blog.Data.Queries;

namespace Blog.Data.Repos
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к обьектам типа Post в базе
    /// </summary>
    public interface IPostRepository
    {
        Task SavePost(Post post, User user, List<Tag> tags);
        Task DeletePost(Post post);
        Task UpdatePost(Post post, UpdatePostQuery query);
        Task<Post[]> GetAllPosts();
        Task<Post[]> GetPostsByUserId(Guid userId);
        Task<Post> GetPostById(Guid id);
    }
}
