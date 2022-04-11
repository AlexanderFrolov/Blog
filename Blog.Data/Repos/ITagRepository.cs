﻿using Blog.Data.Models;


namespace Blog.Data.Repos
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к обьектам типа Tag в базе
    /// </summary>
    public interface ITagRepository
    {
        Task SaveTag(Tag tag);
        Task UpdateTag(Tag tag, string newTag);
        Task DeleteTag(Tag tag);
        Task<Tag[]> GetAllTags();
        Task<Tag> GetTagById(string id);
    }
}
