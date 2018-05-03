using System;
using System.Collections.Generic;
using System.Linq;
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
                routes.MapRoute(
                    name: "recipes",
                    template: "",
                    defaults: new { controller = "Recipes",  action="Recipes"});
                routes.MapRoute(
                    name: "recipeDetail",
                    template: "recipe/{id}",
                    defaults: new { controller = "RecipeDetail",  action="RecipeDetail", id=Guid.Empty});
            });
        }
    }
}
