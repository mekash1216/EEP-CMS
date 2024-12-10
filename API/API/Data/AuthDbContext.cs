using API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define role IDs
            var adminRoleId = Guid.NewGuid().ToString();
            var managerRoleId = "f829f214-d4da-461b-8ac5-3fb572af5f48";
            var doctorRoleId = "0fcedb76-d388-434e-b541-6a59c6ed2f6f";
            var labRoleId = "2d826614-d395-4481-a934-2a47ab65e007";
            var receptionRoleId = "0f02d076-6725-4bc8-ad9f-8303bec44b29";
            var preliminaryRoleId = "f603ccec-be3f-43e9-851e-50374c8f6932";
            var adminUserId = "110abab8-6c64-4aae-9b7e-ac644b48b624";
            var stockId= "C57CBEBB-E5DA-459A-81A1-044C5F478AC7";

            // Seed roles
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {

                 new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId
                },

                       new IdentityRole
                {
                    Id = stockId,
                    Name = "StockMan",
                    NormalizedName = "StockMan",
                    ConcurrencyStamp = stockId
                },

                new IdentityRole
                {
                    Id = managerRoleId,
                    Name = "manager",
                    NormalizedName = "MANAGER",
                    ConcurrencyStamp = managerRoleId
                },
                new IdentityRole
                {
                    Id = labRoleId,
                    Name = "lab",
                    NormalizedName = "LAB",
                    ConcurrencyStamp = labRoleId
                },
                new IdentityRole
                {
                    Id = doctorRoleId,
                    Name = "doctor",
                    NormalizedName = "DOCTOR",
                    ConcurrencyStamp = doctorRoleId
                },
                new IdentityRole
                {
                    Id = receptionRoleId,
                    Name = "reception",
                    NormalizedName = "RECEPTION",
                    ConcurrencyStamp = receptionRoleId
                },
                new IdentityRole
                {
                    Id = preliminaryRoleId,
                    Name = "preliminary",
                    NormalizedName = "PRELIMINARY",
                    ConcurrencyStamp = preliminaryRoleId
                }
            });

            // Create admin user
            //var adminUserId = "110abab8-6c64-4aae-9b7e-ac644b48b624";
          
            var admin = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                IsActive = true // Ensure this is set
            };
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin@123");

            builder.Entity<ApplicationUser>().HasData(admin);

            // Assign roles to admin user
            builder.Entity<IdentityUserRole<string>>().HasData(new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = managerRoleId },
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = doctorRoleId },
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = labRoleId },
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = receptionRoleId },
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = preliminaryRoleId },
                 new IdentityUserRole<string> { UserId = adminUserId, RoleId = stockId },
              new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId },
            });
        }
    }
}
