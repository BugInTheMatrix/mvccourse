using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;
using MyMvcApp.Repositories;
namespace MyMvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ITagInterface _tagRepository;

    public HomeController(ILogger<HomeController> logger, 
        IBlogPostRepository blogPostRepository,
        ITagInterface tagRepository
        )
    {
        _logger = logger;
        _blogPostRepository = blogPostRepository;
        _tagRepository = tagRepository;
    }

    public async Task<IActionResult> Index()
    {
        var blogPost = await _blogPostRepository.GetAllAsync();
        var tags = await _tagRepository.GetAllAsync();
        var homeViewModel = new HomeViewModel
        {
            BlogPosts = blogPost,
            Tags = tags
        };
        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
