using Exam4.DAL;
using Exam4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Exam4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 5;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                opt.Lockout.MaxFailedAccessAttempts = 3;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            var app = builder.Build();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=home}/{action=index}"
            );

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
