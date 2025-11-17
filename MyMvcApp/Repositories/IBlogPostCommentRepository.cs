using MyMvcApp.Models.Domain;
using MyMvcApp.Models.ViewModels;

namespace MyMvcApp.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
        Task<IEnumerable<BlogPostComment>> GetByIdAsync(Guid blogPostId);
        
    }
}
