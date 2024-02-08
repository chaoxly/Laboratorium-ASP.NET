using Laboratorium5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Laboratorium5;

namespace Laboratorium5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDefaultIdentity<IdentityUser>()       
            .AddRoles<IdentityRole>()                             
            .AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IContactService, MemoryContactServices>();
            builder.Services.AddScoped<IAlbumService, EAlbumService>();
            builder.Services.AddScoped<IPiosenkaService, MemoryAlbumServices>();


            var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Album.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=Album.db"));
            /*builder.Services.AddScoped<IAlbumService, EAlbumService>();*/


            builder.Services.AddSession();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();



            app.UseAuthentication();                                 // dodaæ
            app.UseAuthorization();                                  // dodaæ
            app.UseSession();                                        // dodaæ 
            app.MapRazorPages();                                     // dodaæ



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}