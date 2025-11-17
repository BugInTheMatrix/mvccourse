using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MyMvcApp.Data
{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminId = "f58d5be2-0ce2-429c-b356-929d55c16990";
            var superAdminId = "165780a4-689b-468a-9e41-1f954c2c2c3c";
            var userId = "2162a2bc-3c0e-42b2-ae86-367aeff22f15";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName="Admin",
                    Id=adminId,
                    ConcurrencyStamp=adminId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminId,
                    ConcurrencyStamp=superAdminId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName="User",
                    Id=userId,
                    ConcurrencyStamp=userId
                }

            };
            builder.Entity<IdentityRole>().HasData(roles);

            var superAdminUserId = "b5594fb7-6529-401c-8a8e-a77dd5916c3d";

            var superAdminUser = new IdentityUser
            {
                Id = superAdminUserId,
                UserName = "superadmin",
                NormalizedUserName = "SUPERADMIN",
                Email = "superadmin@grown.com",
                NormalizedEmail = "SUPERADMIN@GROWN.COM",

                // ✅ These must be static and hard-coded
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,

                // Add static stamps (DO NOT generate new GUIDs at runtime)
                SecurityStamp = "STATIC-SECURITY-STAMP-123",
                ConcurrencyStamp = "STATIC-CONCURRENCY-STAMP-123"
            };

            // Hash password
            superAdminUser.PasswordHash =
                new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdminPassword123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            var superAdminUserRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId=superAdminId,
                    UserId=superAdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId=adminId,
                    UserId=superAdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId=userId,
                    UserId=superAdminUserId
                }

            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminUserRole);
        }
    }
}
