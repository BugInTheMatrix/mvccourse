using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMvcApp.Models.Domain;
using MyMvcApp.Models.ViewModels;
using MyMvcApp.Repositories;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "User")]
    public class BlogPostUserController : Controller
    {
        public readonly ITagInterface tagRepository;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly UserManager<IdentityUser> userManager;
        public BlogPostUserController(ITagInterface tagRepository, IBlogPostRepository blogPostRepository, UserManager<IdentityUser> userManager)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
            this.userManager = userManager;

        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogsRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogsRequest addBlogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
                UserId = userManager.GetUserId(User)
            };
            List<Tag> selectedTagsList = new List<Tag>();
            foreach (var id in addBlogPostRequest.SelectedTag)
            {
                var seleactedtags = Guid.Parse(id);
                var existingTag = await tagRepository.GetAsync(seleactedtags);
                if (existingTag != null)
                {
                    selectedTagsList.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTagsList;
            var blogpost = await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blog = await blogPostRepository.GetAsyncByUserIdAndBlogId(userManager.GetUserId(User), id);
            if (blog!=null)
            {
                var existintPost = await blogPostRepository.DeleteAsync(id);
            }
            return RedirectToAction("List");

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogs = await blogPostRepository.GetAllAsyncByUserId(userManager.GetUserId(User));
            return View(blogs);

        }
    }
}
