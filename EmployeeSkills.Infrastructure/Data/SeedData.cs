using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSkills.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = new[] { "Admin", "Manager", "Employee" };
            foreach (var role in roles)
            {
                if (!roleMgr.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    roleMgr.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }

            //var adminEmail = "admin@localhost";
            //var adminUser = userMgr.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            //if (adminUser == null)
            //{
            //    adminUser = new ApplicationUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true };
            //    var result = userMgr.CreateAsync(adminUser, "Admin123!").GetAwaiter().GetResult();
            //    if (result.Succeeded)
            //    {
            //        userMgr.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
            //    }
            //}

            //var userEmail = "admin@localhost";
            //var userPassword = "Admin123!";
            //var seededUser = userMgr.FindByEmailAsync(userEmail).GetAwaiter().GetResult();
            //if (seededUser == null)
            //{
            //    seededUser = new ApplicationUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
            //    var createResult = userMgr.CreateAsync(seededUser, userPassword).GetAwaiter().GetResult();
            //    if (createResult.Succeeded)
            //    {
            //        userMgr.AddToRoleAsync(seededUser, "Employee").GetAwaiter().GetResult();
            //    }
            //}
            //else
            //{
            //    var resetPasswordResult = userMgr.RemovePasswordAsync(seededUser).GetAwaiter().GetResult();
            //    if (resetPasswordResult.Succeeded)
            //    {
            //        userMgr.AddPasswordAsync(seededUser, userPassword).GetAwaiter().GetResult();
            //    }
            //}

            //if (seededUser != null && !userMgr.IsInRoleAsync(seededUser, "Employee").GetAwaiter().GetResult())
            //{
            //    userMgr.AddToRoleAsync(seededUser, "Employee").GetAwaiter().GetResult();
            //}
        }
    }
}
