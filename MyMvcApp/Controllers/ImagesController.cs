using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Repositories;
using System.Net;

namespace MyMvcApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            var imageUrl=_imageRepository.UploadAsync(formFile);
            if (imageUrl == null) 
            {
                return Problem("Not found",null, (int)HttpStatusCode.InternalServerError);
            
            }
            return new JsonResult(new { imageUrl });

        }
    }
}
