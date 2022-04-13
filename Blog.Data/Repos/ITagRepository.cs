using Blog.Data.Models;


namespace Blog.Data.Repos
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к обьектам типа Tag в базе
    /// </summary>
    public interface ITagRepository
    {
        Task SaveTag(Tag tag);
        Task UpdateTag(Tag tag, string newName);
        Task DeleteTag(Tag tag);
        Task<Tag[]> GetAllTags();
        Task<Tag> GetTagById(Guid id);
        Task<List<Tag>> GetTagsById(List<Guid> id);
    }
}
