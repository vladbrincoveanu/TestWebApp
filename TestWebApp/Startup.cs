using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using TestWebApp.Data.Repository;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestWebApp.Data.Model;
using TestWebApp.BL;

namespace TestWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add framework services
            // services.AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("LibraryContext"));

            services.AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("LibraryContext"));

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IFavoriteRepository, FavoritesRepository>();
            services.AddSingleton<IPublisher, LendDataProvider>();
            services.AddSingleton<ISubscriber, LendDataSubject>();
            services.AddSingleton<IDocument, Document>();
            services.AddSingleton<GameHistory>();

            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/LogIn/";

                    });
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Run extension method");
            //});
                  

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

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=LogIn}/{id?}");
            });

            DbInitialize.Seed(app);
        }
    }
}
