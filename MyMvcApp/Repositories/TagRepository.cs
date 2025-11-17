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

        public async Task<int> CountAsync()
        {
            return await _myMvcAppDbContext.Tags.CountAsync();
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

        public async Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery,
            string? sortBy,
            string? sortDirection,
            int pageSize=100,
            int pageNumber=1)
        {
            var query = _myMvcAppDbContext.Tags.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query= query.Where(x=>x.Name.Contains(searchQuery)||
                x.DisplayName.Contains(searchQuery));
            }


            //
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                }

                if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
                }
            }
            //

            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            return await query.ToListAsync();
            //var tags = await _myMvcAppDbContext.Tags.ToListAsync();
            //return tags;
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


