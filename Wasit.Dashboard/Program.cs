using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Wasit.Core.Models;
using Wasit.Helpers;

namespace Wasit.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Hosting.Environment = builder.Environment;

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("MsSqlConnectionString");
            builder.Services.AddDbContextServices(builder.Configuration);
            builder.Services.AddDefaultIdentityServices();
            builder.Services.AddControllersWithViews()
                .AddNToastNotifyToastr(new ToastrOptions()
                {
                    ProgressBar = true,
                    PositionClass = ToastPositions.TopRight,
                    PreventDuplicates = true,
                    CloseButton = true,
                    Rtl = true
                });

            builder.Services.AddSingletonServices();
            builder.Services.AddScopedServices();
            builder.Services.AddTransientServices();

            builder.Services.AddLocalizationServices();
            builder.Services.AddCorsServices();
            builder.Services.AddSession();
            builder.Services.TimeOutServices(builder.Environment);
            builder.Services.AddHttpContextAccessor();
            builder.Services.addAutoMapper();
            builder.Services.AddSignalR();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.ScanAllServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var options = ConfigureServices.GetLocalizationOptions();
            app.UseRequestLocalization(options);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("Wasit");

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
