
using AutoMapper;
using DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceLayer.AutoMapper;
using ServiceLayer.Services.BlogServices;
using ServiceLayer.Services.ProductServices;
using ServiceLayer.Services.SiteServices;
using StackExchange.Profiling.Storage;
using System;
using System.Net;
using System.Threading.Tasks;
using UI.Constraint;
using UI.middleware;

namespace UI
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ShopDbContext>(cng =>
            {
                //cng.UseSqlServer("Server =mssql.kwtool.ir; Database=shop;user id =kwtool.ir;password=VwA!J6h381s;MultipleActiveResultSets=true;");
                cng.UseSqlServer(configuration["connectionstring"]);
            });

            services.AddScoped<MyMapper, AutoMapperConfig>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();




            services.AddMiniProfiler(options => options.RouteBasePath = "/profiler").AddEntityFramework();


        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseMiddleware<BanIpsMiddleware>();
            //app.UseMiddleware<SiteViewMiddleware>();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseMiniProfiler();
            //}
            app.UseExceptionHandler("/Home/Error");
            //   app.UseDeveloperExceptionPage();


            app.UseStatusCodePages(handler: context =>
            {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;
                if (response.StatusCode != 200)
                {
                    response.Redirect($"/error/{response.StatusCode}");
                }

                return Task.CompletedTask;
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "categorysearch",
                    pattern: "ProductSearch/{id}",
                    defaults: new { Controller = "product", action = "CategoryProductSearch" },
                    constraints: new { id = @"\d+" }
                    );

                endpoints.MapControllerRoute(
                    name: "ProductSearch",
                    pattern: "ProductSearch/{keyword}",
                    defaults: new { Controller = "product", action = "ProductSearch" },
                    constraints: new { keyword = new BanScriptRouteConstraint() }
                    );


                endpoints.MapControllerRoute(
                    name: "Article",
                    pattern: "article/{postid}/{slug}",
                    defaults: new { Controller = "Blog", action = "GetPost" },
                    constraints: new { postid = @"\d+", slug = new BanScriptRouteConstraint() }
                    );

                endpoints.MapControllerRoute(
                    name: "Product",
                    pattern: "product/{id}/{slug}",
                    defaults: new { controller = "product", action = "ShowProduct" },
                    constraints: new { id = @"\d+", slug = new BanScriptRouteConstraint() }
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
