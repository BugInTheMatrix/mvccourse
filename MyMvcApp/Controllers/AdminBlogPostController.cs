using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMvcApp.Models.Domain;
using MyMvcApp.Models.ViewModels;
using MyMvcApp.Repositories;

namespace MyMvcApp.Controllers
{
    public class AdminBlogPostController : Controller
    {
        public readonly ITagInterface tagRepository;
        private readonly IBlogPostRepository blogPostRepository;
        public AdminBlogPostController(ITagInterface tagRepository,IBlogPostRepository blogPostRepository) 
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogsRequest
            {
                Tags = tags.Select(x=> new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() } )
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
            };
            List<Tag> selectedTagsList= new List<Tag>();
            foreach (var id in addBlogPostRequest.SelectedTag) 
            {
                var seleactedtags= Guid.Parse(id);
                var existingTag= await tagRepository.GetAsync(seleactedtags);
                if (existingTag != null) 
                {
                    selectedTagsList.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTagsList;
            var blogpost = await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogs = await blogPostRepository.GetAllAsync();
            return View(blogs);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (blogPost != null)
            {
                // map the domain model into the view model
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);

            }

            // pass data to view
            return View(null);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            // map view model back to domain model
            var blogPostDomainModel = new BlogPost
            {
                id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible
            };

            // Map tags into domain model

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;

            // Submit information to repository to update
            var updatedBlog =  await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if (updatedBlog != null)
            {
                // Show success notification
                return RedirectToAction("Edit");
            }

            // Show error notification
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            if(editBlogPostRequest != null)
            {
                var existintPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id=editBlogPostRequest.Id});
        }

    }
}