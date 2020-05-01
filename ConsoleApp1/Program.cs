using AutoMapper;
using DataAccess.Context;
using EntityModels.Entities.Categories;
using EntityModels.Entities.Posts;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ServiceLayer.AutoMapper;
using ServiceLayer.Services.BlogServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {


        public static ServiceProvider Services { get; set; }

        class ProxsyApi
        {
            public string ip { get; set; }
            public string port { get; set; }
        }

        //static async Task<bool> Run()
        //{
        //    //  return true;
        //    var c = new ShopDbContext();
        //    var post = c.Post.Find(1);
        //    post.Title = "faraz fg qweqw";

        //    //c.Post.Add(new Post()
        //    //{
        //    //    FullText = "wqeqw",
        //    //    Title = "farshid jahanmanesh",
        //    //    IsActive = true,
        //    //    PublishDate = DateTime.Now,
        //    //    Summery = "wqeqw"
        //    //});
        //    c.SaveChanges();
        //    BlogService blogService = new BlogService(new ShopDbContext());

        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    // ActivateComments(blogService);
        //    //    await blogService.SaveChangesAsync();
        //    sw.Stop();

        //    //blogService.DeleteComment(3);
        //    //await blogService.AddCommentAsync(new PostComment()
        //    //{
        //    //    Email="qwe",
        //    //    IsActive=true,
        //    //    Name="qweqw",
        //    //    PostId=1,
        //    //    PublishDate=DateTime.Now,
        //    //    Text="qweqqwe"
        //    //});

        //    //await blogService.SaveChangesAsync();
        //    Console.WriteLine("Hello World!");
        //    return true;
        //}

        private static void ActivateComments(BlogService blogService)
        {
            blogService.ActivateComments(new List<PostComment>()
            {
                new PostComment
                {
                    Id=1,IsActive=true,PostId=1
                },


                  new PostComment
                {
                    Id=4,IsActive=true,PostId=1
                },
                   new PostComment
                {
                    Id=5,IsActive=true,PostId=1
                },
                    new PostComment
                {
                    Id=6,IsActive=true,PostId=1
                },
                     new PostComment
                {
                    Id=7,IsActive=true,PostId=1
                },
                      new PostComment
                {
                    Id=8,IsActive=true,PostId=1
                },
                       new PostComment
                {
                    Id=9,IsActive=true,PostId=1
                }
            });
        }

        //public async static Task ProxsySetter()
        //{
        //    WebClient web = new WebClient();
        //    var test = 0;

        //    for (int i = 0; i < 1000; i++)
        //    {
        //        try
        //        {

        //            var data = web.DownloadString("https://api.getproxylist.com/proxy?allowsHttps=1");
        //            var des = JsonConvert.DeserializeObject<ProxsyApi>(data);
        //            var proxsadd = des.ip + ":" + des.port;

        //            WebProxy mypro = new WebProxy(proxsadd, false);
        //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://kwtool.ir/");
        //            request.Proxy = mypro;
        //            request.Method = "Get";
        //            var response = await request.GetResponseAsync();
        //            test++;
        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //    }
        //    //var data= web.DownloadString("https://api.getproxylist.com/proxy");
        //    // var des=JsonConvert.DeserializeObject<ProxsyApi>(data);

        //    //   HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://facebook.com");
        //    //WebProxy mypro = new WebProxy("91.52.31.23:8080", false);
        //    //request.Proxy = mypro;
        //    //request.Method = "GET";
        //    //HttpWebResponse response = (HttpWebResponse)request.GetResponse();


        //    //WebProxy mypro = new WebProxy("91.52.31.23:8080", false);
        //    //web.Proxy = mypro;
        //    var s = web.DownloadString("https://facebook.com");

        //    //  var res =response.GetResponseStream();
        //}

        static void Main(string[] args)
        {
            Services = new ServiceCollection()
            .AddTransient<MyMapper, AutoMapperConfig>()
            .AddTransient<ShopDbContext, ShopDbContext>()
            .BuildServiceProvider();
            UpdatePost().Wait();
            Console.WriteLine("hello");
        }

        private async static Task UpdatePost()
        {

            BlogService b = new BlogService(Services.GetService<ShopDbContext>(), Services.GetService<MyMapper>());
          // b.DeletePost(new EntityModels.DTOs.PostDto() { 
          
          //Id=149
          //});
          // await b.SaveChangesAsync();
            //{
            //    CancellationTokenSource source = new CancellationTokenSource();
            //    CancellationToken token = source.Token;

            //    BlogService b = new BlogService(Services.GetService<ShopDbContext>(), Services.GetService<MyMapper>());

            //    TaskStatus statud = await b.AddPost(new EntityModels.DTOs.PostDto()
            //    {
            //        Title = "eqw",
            //        PublishDate = DateTime.Now,
            //        IsActive = false,
            //       FullText = "qweqw",
            //        Summery="drfgd",
            //        AuthorId=1
            //    });
            //   await b.SaveChangesAsync();

            b.UpdatePost(new EntityModels.DTOs.PostDto()
            {
                Id = 4,
                Title = "farshiasdqwd jahanmanesh",
                 PublishDate = DateTime.Now,
                //AuthorId=1
            });
            await b.SaveChangesAsync();
        }

        //private async static Task AddPost()
        //{
        //    BlogService b = new BlogService(Services.GetService<ShopDbContext>(), Services.GetService<MyMapper>());
        //    TaskStatus statud =await b.AddPost(new EntityModels.DTOs.PostDto()
        //    {
        //        Title = "eqw",
        //        PublishDate = DateTime.Now,
        //        IsActive = false,
        //       // FullText = "qweqw",Summery="drfgd"
        //        //AuthorId=1
        //    });
        //   await b.SaveChangesAsync();
        //}
    }
}
