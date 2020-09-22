using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AnimalCatalogSqLite.Repositories;
using AnimalCatalogSqLite.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AnimalCatalogSqLite.Models;
using Microsoft.Extensions.Configuration;

namespace AnimalCatalogSqLite
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AddAuth(services);
            services.AddDbContext<CatalogContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnection"))
            );
            
            services.AddTransient<IAnimalRepository, AnimalRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, UserManager<IdentityUser> userManager, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("ExternalProduction"))
            {
                app.UseExceptionHandler("/Error");
            }

            DbSeeder.SeedUsers(userManager);
            
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Invalid URL!");
            });
        }

        public void AddAuth(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<CatalogContext>();

        }

        //public async void CreateRoles(RoleManager<IdentityRole> roleManager)
        //{
        //    string[] roleNames = { "Adm", "Manager", "User" };

        //    foreach (string roleName in roleNames)
        //    {
        //        bool roleExists = await roleManager.RoleExistsAsync(roleName);
        //        if (!roleExists)
        //        {
        //            IdentityRole role = new IdentityRole() { Name = roleName };
        //            await roleManager.CreateAsync(role);
        //        }
        //    }
        //}
    }
}
