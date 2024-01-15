using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskHubAPI.Context;
using TaskHubAPI.Services.User;
using TaskHubAPI.src.Services.Tasks;

namespace TaskHubAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                });
                services.AddDbContext<AppDbContext>();
            services.AddScoped<TaskService>();
            services.AddScoped<UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllerRoute("default",
                "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
