using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;

namespace MyMvcApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _authDbContext;
        public UserRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAllAsync()
        {
            var users = await _authDbContext.Users.ToListAsync();
            var superAdmin = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@grown.com");
            if (superAdmin != null)
            {
                users.Remove(superAdmin);
            }
            return users;
        }
    }
}
