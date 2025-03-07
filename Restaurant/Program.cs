using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            //SESSION
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //DATABASE
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestaurantSystem;Trusted_Connection=True;");
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}