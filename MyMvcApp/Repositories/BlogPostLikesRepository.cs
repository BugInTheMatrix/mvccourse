
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models.Domain;

namespace MyMvcApp.Repositories
{
    public class BlogPostLikesRepository : IBlogPostLikeRepository
    {
        private readonly MyMvcAppDbContext _dbContext;
        public BlogPostLikesRepository(MyMvcAppDbContext myMvcAppDbContext)
        {
            _dbContext = myMvcAppDbContext;
        }
        public async Task<int> GetAllLikes(Guid blogPostId)
        {
            return await _dbContext.BlogPostLikes.CountAsync(x=>x.BlogPostId == blogPostId);
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _dbContext.BlogPostLikes.AddAsync(blogPostLike);
            await _dbContext.SaveChangesAsync();
            return blogPostLike;

        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await _dbContext.BlogPostLikes.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }
    }
}
