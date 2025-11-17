using MyMvcApp.Models.Domain;

namespace MyMvcApp.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetAllLikes(Guid blogPostId);
        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
