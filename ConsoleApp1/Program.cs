using DataAccess.Context;
using DataAccess.UnitOfWork;
using EntityModels.Entities.Categories;
using ServiceLayer.Repositories;
using ServiceLayer.Repositories.BaseRepository;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task<bool> Run()
        {
            using (var ctx = new ShopDbContext())
            {
                // ctx.Database.EnsureCreated();
                CategoryRepository cr = new CategoryRepository(new UnitOfWork(ctx));
               await cr.AddAsync(new Category()
                {
                    IsActive = true,
                    Text = "farshidjahanjj"
                });
                //ProductGroupsRepository pr = new ProductGroupsRepository(ctx);
                //await pr.AddAsync(new ProductGroups()
                //{
                //    CategoryId = 4,
                //    IsActive = false,
                //    Text = "hiiiii"
                //});
                //    ICategoryRepository category = new CategoryRepository(ctx);
                //var result = await category.AddAsync(new Category()
                //{
                //    IsActive = true,
                //    Text = "hello"
                //});
                // var b= category.Remove(2);
                //    var update = category.Remove(4);
                //var result=await category.AddProductGroups(new ProductGroups()
                //{
                //    CategoryId=5,
                //    IsActive=false,
                //    Text="hiiiii"
                //});
                //    var Saveresult=await category.SaveChangesAsync();
                return true;
            }
            Console.WriteLine("Hello World!");
        }
        static void Main(string[] args)
        {
            bool result = Run().Result;
            Console.WriteLine("hello");
        }
    }
}
