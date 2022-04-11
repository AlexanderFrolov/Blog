using Blog.Data.Models;
using Blog.Data.Queries;


namespace Blog.Data.Repos
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к обьектам типа User в базе
    /// </summary>
    public interface IUserRepository
    {
        Task RegisterUser(User user);
        Task DeleteUser(User user);
        Task UpdateUser(User user, UpdateUserQuery query); 
        Task<User> GetUser(Guid id);
        Task<User[]> GetUsers();
    }
}
