using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models.Domain;
using MyMvcApp.Models.ViewModels;
using MyMvcApp.Repositories;

namespace MyMvcApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var model =new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId
            };
            var modelnew = await _blogPostLikeRepository.AddLikeForBlog(model);
            return Ok();
        }
        [HttpGet]
        [Route("{blogPostId:guid}/totalLikes")]
        public async Task<IActionResult> GetAllLikes([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _blogPostLikeRepository.GetAllLikes(blogPostId);
            return Ok(totalLikes);
        }
    }
}
