using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository productRepository { get; }
        IProductGroupsRepository productGroupsRepository { get; }
        ICategoryRepository categoryRepository { get; }
        ShopDbContext DB { get;  }

        Task<int> Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ShopDbContext _db)
        {
            DB = _db;
        }

        public IProductRepository productRepository { get => new ProductRepository(this); }
        public IProductGroupsRepository productGroupsRepository { get => new ProductGroupsRepository(this); }
        public ICategoryRepository categoryRepository { get => new CategoryRepository(this); }
        public ShopDbContext DB
        {
            get;private set;
        }

        public async Task<int> Save()
        {
            var result =await DB.SaveChangesAsync();
            return result;
        }
    }

}
