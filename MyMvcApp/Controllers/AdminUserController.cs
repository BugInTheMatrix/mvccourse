using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models.ViewModels;
using MyMvcApp.Repositories;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        public readonly IUserRepository UserRepository;
        private readonly UserManager<IdentityUser> userManager;
        public AdminUserController(IUserRepository userRepository,UserManager<IdentityUser> userManager)
        {
            this.UserRepository = userRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await UserRepository.GetAllAsync();
            UsersViewModel viewModel = new UsersViewModel();
            viewModel.users=new List<User>();
            foreach (var tempUser in users) 
            {
                viewModel.users.Add(new User
                {
                    Id=Guid.Parse(tempUser.Id),
                    UserName=tempUser.UserName,
                    Email=tempUser.Email
                });
            
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UsersViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };


            var identityResult =
                await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    // assign roles to this user
                    var roles = new List<string> { "User" };

                    if (request.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }

                    identityResult =
                        await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult is not null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUser");
                    }

                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var tempuser = await userManager.FindByIdAsync(Id.ToString());

            if (tempuser is not null)
            {
                var identityResult = await userManager.DeleteAsync(tempuser);

                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUser");
                }
            }

            return RedirectToAction("List", "AdminUser");


        }
    }
}
