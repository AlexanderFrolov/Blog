using Blog.Data.Models;

namespace Blog.Data.Repos
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к обьектам типа Role в базе
    /// </summary>
    public interface IRoleRepository
    {
        Task SaveRole(Role role);
        Task UpdateRole(Role role, string newName, string newDescription);
        Task DeleteRole(Role role);
        Task<Role[]> GetAllRoles();

        //Task<Tag> GetTagById(Guid id);
        //Task<List<Tag>> GetTagsById(List<Guid> id);
        //Task<Tag[]> GetTagsByUserId(Guid id);
    }
}
