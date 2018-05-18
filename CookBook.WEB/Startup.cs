using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CookBook.BL.Facades;
using CookBook.BL.Models;
using CookBook.BL.Queries;
using CookBook.BL.Repositories;
using CookBook.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.WEB
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
            services.AddMvc();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<CookBookDbContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("CookBookDatabase")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<GetAllRecipesQuery>();
            services.AddTransient<RecipeRepository>();
            services.AddTransient<RecipeFacade>();
            services.AddTransient<Func<CookBookDbContext>>(provider => provider.GetService<CookBookDbContext>);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("Recipes", "{controller=Recipes}/{action=Index}");
                routes.MapRoute("RecipeNew","Recipes/New",defaults:new { controller = "Recipes", action = "Detail"});
                routes.MapRoute("RecipeDetail", "Recipes/Detail/{id:guid}", defaults: new { controller = "Recipes", action = "Detail" });
                routes.MapRoute("RecipeDelete", "Recipes/Delete/{id:guid}", defaults: new { controller = "Recipes", action = "Delete" });
            });
        }
    }
}
