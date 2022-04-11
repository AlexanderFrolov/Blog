﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Data.Models;
using Blog.Data.Queries;

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
        public async Task SaveTag(Tag tag)
        {
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
        public async Task<Tag> GetTagById(string id)
        {
            return await _context.Tags.Where(t => t.TagId == id).FirstOrDefaultAsync();
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
        public async Task UpdateTag(Tag tag, string newTag)
        {
            if(!string.IsNullOrEmpty(newTag))
                tag.TagId = newTag;

            // add to db
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                _context.Tags.Update(tag);

            await _context.SaveChangesAsync();
        }
    }
}
