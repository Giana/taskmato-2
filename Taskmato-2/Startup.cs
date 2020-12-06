using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskmato_2.Data;
using Taskmato_2.Data.Services;

namespace Taskmato_2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var initConfig = new Config();
            Configuration.GetSection("Secrets").Bind(initConfig);

            services.AddSession();
            services.AddRazorPages();

            services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<TaskmatoContext>();


            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Session/Index";
                options.SlidingExpiration = true;
            });

            services.AddScoped<TaskmatoInitializer>();
            services.AddScoped<ITaskmatoService, TaskmatoService>();
            services.AddScoped<ITaskListService, TaskListService>();
            services.AddScoped<IUserService, UserService>();
            services.AddControllersWithViews();

            services.AddDbContext<TaskmatoContext>(options => options.UseSqlServer(
                initConfig.DatabaseDSN, providerOptions => providerOptions.EnableRetryOnFailure()
                ));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TaskmatoInitializer dataInit)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=TaskList}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
