using System;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models.Domain;

namespace MyMvcApp.Repositories
{
    public class TagRepository : ITagInterface
    {
        private readonly MyMvcAppDbContext _myMvcAppDbContext;
        public TagRepository(MyMvcAppDbContext myMvcAppDbContext)
        {
            this._myMvcAppDbContext = myMvcAppDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _myMvcAppDbContext.AddAsync(tag);
            await _myMvcAppDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?>DeleteAsync(Guid id)
        {
            var existingTag =await _myMvcAppDbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                _myMvcAppDbContext.Tags.Remove(existingTag);
                await _myMvcAppDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await _myMvcAppDbContext.Tags.ToListAsync();
            return tags;
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _myMvcAppDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _myMvcAppDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await _myMvcAppDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}


