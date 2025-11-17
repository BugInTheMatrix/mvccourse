using Microsoft.AspNetCore.Identity;

namespace MyMvcApp.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllAsync();
    }
}
