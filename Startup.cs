using MvcStartApp.Middleware;
using MvcStartApp.Models.Context;
using MvcStartApp.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace MvcStartApp
{
    public class Startup
    {
        // Метод вызывается средой ASP.NET.
        // Используйте его для подключения сервисов приложения
        // Документация:  https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;User ID=sa;Password=admin-admin;TrustServerCertificate=true;";
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
            // регистрация сервиса репозитория для взаимодействия с базой данных
            //services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddDbContext<LogContext>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
            services.AddScoped<ILogRepository, LogRepository>();
        }

        // Метод вызывается средой ASP.NET.
        // Используйте его для настройки конвейера запросов
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Поддержка статических файлов
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Подключаем логирвоание с использованием ПО промежуточного слоя
            app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });*/
        }

    }

}
