using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace HealthCare340B.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var defaultDateCulture = "id-ID";
            var ci = new CultureInfo(defaultDateCulture);

            // Configure the Localization middleware
            app.UseRequestLocalization(
                new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(ci),
                    SupportedCultures = new List<CultureInfo> { ci },
                    SupportedUICultures = new List<CultureInfo> { ci },
                }
            );

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
