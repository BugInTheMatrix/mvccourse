using System;
using MyMvcApp.Models.Domain;

namespace MyMvcApp.Repositories
{
    public interface ITagInterface
    {
        Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery=null,
            string? sortBy=null,
            string? sortDirection=null,
            int pageSize=100,
            int pageNumber = 1);
        Task<Tag?> GetAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
        Task<int> CountAsync();
    }  
}


