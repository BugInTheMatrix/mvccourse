using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using MyMvcApp.Models.Domain;
using MyMvcApp.Models.ViewModels;
using MyMvcApp.Repositories;

namespace MyMvcApp.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        public BlogsController(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blog= await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var liked = false;
            if(blog !=null)
            {
                var totallikes = await blogPostLikeRepository.GetAllLikes(blog.id);
                if (signInManager.IsSignedIn(User))
                {
                    // Get like for this blog for this user
                    var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blog.id);

                    var userId = userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }
                var blogslist = await blogPostCommentRepository.GetByIdAsync(blog.id);
                var blogCommentTemp = new List<BlogComment>();
                foreach (var blogTemp in blogslist) 
                {
                    blogCommentTemp.Add(new BlogComment
                    {
                        CommentDescription = blogTemp.Description,
                        UserName = (await userManager.FindByIdAsync(blogTemp.UserId.ToString())).UserName
                    });
                
                }

                var newBlogIwthlikes = new BlogPostLikesViewModel
                {
                    id = blog.id,
                    Heading = blog.Heading,
                    PageTitle = blog.PageTitle,
                    Content = blog.Content,
                    ShortDescription = blog.ShortDescription,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    UrlHandle = blog.UrlHandle,
                    PublishedDate = blog.PublishedDate,
                    Author = blog.Author,
                    Visible = blog.Visible,
                    Tags = blog.Tags,
                    TotalLikes = totallikes,
                    Liked=liked,
                    BlogComments= blogCommentTemp
                };
                return View(newBlogIwthlikes);

            }
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogPostLikesViewModel blogPostLikesViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var blogPostComment = new BlogPostComment
                {
                    BlogPostId = blogPostLikesViewModel.id,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    Description = blogPostLikesViewModel.CommentsDescription
                };
                await blogPostCommentRepository.AddAsync(blogPostComment);
                return RedirectToAction("Index","Blogs",new {urlHandle = blogPostLikesViewModel.UrlHandle});
            }
            
            return View();
        }

    }
}
