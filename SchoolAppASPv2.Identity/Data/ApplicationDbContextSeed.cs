using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolAppASPv2.Identity.Models;

namespace SchoolAppASPv2.Identity.Data
{
    public class ApplicationDbContextSeed
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();
        public async Task SeedAsync(ApplicationDbContext context, IWebHostEnvironment env,
            ILogger<ApplicationDbContextSeed> logger)
        {
            try
            {
                var adminRole = new IdentityRole("Admin");

                if (context.Roles.All(r => r.Name != adminRole.Name))
                {
                    adminRole.NormalizedName = adminRole.Name.ToUpper();
                    await context.Roles.AddAsync(adminRole);
                }

                var enddUser = new IdentityRole("EndUser");

                if (context.Roles.All(r => r.Name != enddUser.Name))
                {
                    enddUser.NormalizedName = enddUser.Name.ToUpper();

                    await context.Roles.AddAsync(enddUser);
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(GetDefaultUser());
                    await context.SaveChangesAsync();
                }

                foreach (var user in await context.Users.Where(x => x.Name == "DemoUser").ToListAsync())
                {
                    IdentityUserRole<string> iur = new IdentityUserRole<string>
                    {
                        RoleId = adminRole.Id,
                        UserId = user.Id
                    };

                    await context.UserRoles.AddAsync(iur);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(ApplicationDbContext));
            }
        }
        private IEnumerable<ApplicationUser> GetDefaultUser()
        {
            var user =
            new ApplicationUser()
            {

                City = "Redmond",
                Country = "U.S.",
                Email = "demouser@microsoft.com",
                Id = "081C459B-77D3-45F3-B26A-6879EAE53FC2",
                LastName = "DemoLastName",
                Name = "DemoUser",
                PhoneNumber = "1234567890",
                UserName = "demouser@microsoft.com",
                ZipCode = "98052",
                State = "WA",
                Street = "15703 NE 61st Ct",
                NormalizedEmail = "DEMOUSER@MICROSOFT.COM",
                NormalizedUserName = "DEMOUSER@MICROSOFT.COM",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, "Pass@word1");

            return new List<ApplicationUser>()
            {
                user
            };
        }
    }
}

