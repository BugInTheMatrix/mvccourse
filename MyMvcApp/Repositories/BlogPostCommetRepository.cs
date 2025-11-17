using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models.Domain;
using MyMvcApp.Models.ViewModels;

namespace MyMvcApp.Repositories
{
    public class BlogPostCommetRepository : IBlogPostCommentRepository
    {
        private readonly MyMvcAppDbContext _context;
        public BlogPostCommetRepository(MyMvcAppDbContext myMvcAppDbContext) 
        {
            _context=myMvcAppDbContext;


        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await _context.BlogPostComments.AddAsync(blogPostComment);
            await _context.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetByIdAsync(Guid blogPostId)
        {
            return await _context.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
