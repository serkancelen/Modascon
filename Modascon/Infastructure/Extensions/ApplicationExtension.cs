using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Modascon.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            RepositoryContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RepositoryContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-TR", "en-EN")
                .AddSupportedCultures("tr-TR")
                .SetDefaultCulture("tr-TR");
            });
        }
        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminUser = "Admin";
            const string adminPassword = "5LQAcMKS.";

            UserManager<IdentityUser> userManager = app
                .ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser(adminUser)
                {
                    Email = "serkancelen@outlook.com.tr",
                    PhoneNumber = "5522451575",
                    UserName = adminUser,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception("Admin kullanıcısı oluşturulmadı");
                }
                
                var roleResult = await userManager.AddToRolesAsync(user, roleManager.Roles.Select(r => r.Name).ToList());
                if (!roleResult.Succeeded)
                {
                    throw new Exception("Admin Rolleri ile alakalı sorun oluştu");
                }
            }
        }
    }
}
