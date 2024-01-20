using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TaskHubAPI.Context;
using TaskHubAPI.Services.User;
using TaskHubAPI.src.Services.Tasks;

namespace TaskHubAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo{Title = "TaskHubAPI", Version = "v1"});
            });

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
                
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskHubAPI v1"));
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
