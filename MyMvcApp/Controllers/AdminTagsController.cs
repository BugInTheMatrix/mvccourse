using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models.Domain;
using MyMvcApp.Models.ViewModels;
using MyMvcApp.Repositories;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagInterface _tagRepository;
        public AdminTagsController(ITagInterface tagRepository)
        {
            this._tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTagsRequest addTagsRequest)
        {
            ValidateParametersCustom(addTagsRequest);
            if (!ModelState.IsValid)
            {
                return View();
            }
            Tag tag = new Tag
            {
                Name = addTagsRequest.Name,
                DisplayName = addTagsRequest.DisplayName
            };
            await _tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List(
            string? searchQuery,
            string? sortBy,
            string? sortDirection,
            int pageSize=3,
            int pageNumber=1)
        {
            var totalRecords= await _tagRepository.CountAsync();
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);
            if (pageNumber > totalPages)
            {
                pageNumber--;
            }
            if (pageNumber < 1)
            {
                pageNumber++;
            }
            ViewBag.TotalPages = totalPages;
            ViewBag.searQuery = searchQuery;
            ViewBag.sortBy = sortBy;
            ViewBag.sortDirection = sortDirection;
            ViewBag.pageSize = pageSize;
            ViewBag.pageNumber = pageNumber;
            var tags=await _tagRepository.GetAllAsync(searchQuery,sortBy,sortDirection,pageSize,pageNumber);
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st method
            // Tag tag = _myMvcAppDbContext.Tags.Find(id);
            //2nd method
            Tag? tag = await _tagRepository.GetAsync(id);
            if (tag != null)
            {
                EditTagsRequest editTagsRequest = new EditTagsRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagsRequest);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagsRequest editTagsRequest)
        {
            Tag tag = new Tag
            {
                Id = editTagsRequest.Id,
                Name = editTagsRequest.Name,
                DisplayName = editTagsRequest.DisplayName
            };

            var tagupdated = await _tagRepository.UpdateAsync(tag);
            if (tagupdated != null)
            {
                //show success

            }
            else
            {
                //show error
            }
            // Show unsuccessfull
            return RedirectToAction("Edit", new { id = editTagsRequest.Id });
            // return RedirectToAction("List");


        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagsRequest editTagsRequest)
        {
            var existingTag =await _tagRepository.DeleteAsync(editTagsRequest.Id);
            if (existingTag != null)
            {
                return RedirectToAction("List");
            }
            // show error notification
            return RedirectToAction("Edit", new { id = editTagsRequest.Id });
            

        }
        public void ValidateParametersCustom(AddTagsRequest addTagsRequest)
        {
            if (addTagsRequest.Name != null && addTagsRequest.DisplayName != null) 
            {
                if (addTagsRequest.Name==addTagsRequest.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name should Not be equal to display name");
                }
            
            }
        }

    }
}
