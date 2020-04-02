using EntityModels.Entities.Products;
using ServiceLayer.Repositories.BaseRepository;
using System;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Repositories;
using System.Threading.Tasks;

namespace ServiceLayer.Test
{
    public class BaseRepositoryTest
    {

        [Fact]
        public void t()
        {

            Mock<DbContext> mock = new Mock<DbContext>();
            Mock<DbSet<Product>> dbset = new Mock<DbSet<Product>>();
            dbset.SetupAllProperties();
            dbset.Object.Add(new Product()
            {
                Name = "bla",
                Price = 3000,
                GroupId = 3,
                Count = 4
            });
            mock.Setup(x => x.Set<Product>()).Returns(dbset.Object);

            ProductRepository product = new ProductRepository(mock.Object);
            
            var t=product.AddAsync(new Product()
            {
                Name = "bla",
                Price = 3000,
                GroupId = 3,
                Count = 4
            }).Result;

            Assert.True(t);


        }


        [Fact]
        public void Test1()
        {
            Mock<DbContext> mock = new Mock<DbContext>();
            
            ProductRepository product = new ProductRepository(mock.Object);
            
            Mock<IBaseRepository<Product>> pro = new Mock<IBaseRepository<Product>>();
            pro.Setup(x => x.AddAsync(It.IsAny<Product>())).Returns(Task.FromResult(true));

            Mock<DbSet<Product>> dbset = new Mock<DbSet<Product>>();
            dbset.Object.Add(new Product());
          //  pro.Setup(x => x._repo).Returns(dbset.Object);
            
            var r2 = pro.Object.AddAsync(new Product()
            {
                Name = "bla",
                Price = 3000,
                GroupId = 3,
                Count = 4

            }).Result;

            Assert.True(r2);

        }
    }
}
