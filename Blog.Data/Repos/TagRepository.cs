using Microsoft.EntityFrameworkCore;
using Blog.Data.Models;

namespace Blog.Data.Repos
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogContext _context;

        public TagRepository(BlogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// add new Tag
        /// </summary>
        public async Task SaveTag(Tag tag, User user)
        {
            tag.User = user;

            var entry = _context.Entry(tag);

            if (entry.State == EntityState.Detached)
                await _context.Tags.AddAsync(tag);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete tag
        /// </summary>
        public async Task DeleteTag(Tag tag)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get tag by id
        /// </summary>
        public async Task<Tag> GetTagById(Guid id)
        {
            return await _context.Tags.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get tag by id
        /// </summary>
        public async Task<Tag[]> GetTagsByUserId(Guid id)
        {
            return await _context.Tags.Include(u => u.User).Where(u => u.User.Id == id).ToArrayAsync();
        }
        

        /// <summary>
        /// get tags by id.  returns array tags.
        /// </summary>
        public async Task<List<Tag>> GetTagsById(List<Guid> ids)
        {
            return await _context.Tags.Include(p => p.Posts).Where(c => ids.Contains(c.Id)).ToListAsync();
        }   

        /// <summary>
        /// get all tags
        /// </summary>
        public async Task<Tag[]> GetAllTags()
        {
            return await _context.Tags.ToArrayAsync();
        }

        /// <summary>
        /// update tag
        /// </summary>
        public async Task UpdateTag(Tag tag, string newName)
        {
            if(!string.IsNullOrEmpty(newName))
                tag.Name = newName;

            // add to db
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                _context.Tags.Update(tag);

            await _context.SaveChangesAsync();
        }
    }
}
