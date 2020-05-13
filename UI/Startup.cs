
using AutoMapper;
using DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLayer.AutoMapper;
using ServiceLayer.Services.BlogServices;
using ServiceLayer.Services.ProductServices;
using ServiceLayer.Services.SiteServices;
using System;

namespace UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ShopDbContext>(cng =>
            {

                cng.UseSqlServer("Server = .; Database = MyShopTestDb; Trusted_Connection = True;");
            });
            services.AddScoped<MyMapper, AutoMapperConfig>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {

            }
            app.UseStaticFiles();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"ProductSearch",
                    pattern: "ProductSearch/{keyword}",
                    defaults: new { Controller = "product",action= "ProductSearch" }
                    );
                endpoints.MapControllerRoute(
                    name: "Article",
                    pattern: "article/{postid}/{slug}",
                    defaults: new { Controller = "Blog", action = "GetPost" },
                    new { postid = @"\d+" }
                    );

                endpoints.MapControllerRoute(
                    name: "Product",
                    pattern: "product/{id}/{slug}",
                    defaults: new { controller = "product", action = "ShowProduct" },
                    new { id = @"\d+" }
                    );

                endpoints.MapControllerRoute(
                    name: "defualt",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "home", action = "index" }
                    );
            });
        }
    }
}
