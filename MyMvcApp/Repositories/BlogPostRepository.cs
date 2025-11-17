using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models.Domain;
namespace MyMvcApp.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private MyMvcAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;    
        public BlogPostRepository(MyMvcAppDbContext myMvcAppDbContext,UserManager<IdentityUser> userManager) 
        {
            _context = myMvcAppDbContext;
            _userManager = userManager;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog=await _context.BlogPosts.FindAsync(id);
            if (existingBlog != null) 
            {
                _context.BlogPosts.Remove(existingBlog);
               await _context.SaveChangesAsync();
                return existingBlog;
            
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await _context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.id==id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle==urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await _context.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.id == blogPost.id);

            if (existingBlog != null)
            {
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await _context.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsyncByUserId(string userId)
        {
            return await _context.BlogPosts.Include(x=>x.Tags).Where(x=>x.UserId==userId).ToListAsync();

        }
        public async Task<BlogPost?> GetAsyncByUserIdAndBlogId(string userId,Guid id)
        {
            return await _context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UserId == userId && x.id==id);

        }
    }
}
