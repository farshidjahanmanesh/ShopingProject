using DataAccess.UnitOfWork;
using EntityModels.Entities.Products;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductByName(string name);
        Task<List<Product>> GetProducts(int page, int count, bool IsActive);

        Task<List<Product>> GetProductsByGroupId(int GroupId, bool IsActive);
    }
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        //base repo in this Repository is a instance of category DbSet

       // private readonly DbContext _ctx;
        public ProductRepository(IUnitOfWork UW) : base(UW)
        {
           // this._ctx = _ctx;

        }
        public async override Task<bool> AddAsync(Product entity)
        {
            try
            {
                if (entity.GroupId == null || entity.GroupId == 0)
                {
                    return false;
                }
                if (entity.Price == 0 || entity.Price < 0)
                {
                    return false;
                }
                if (entity.Count < 0)
                {
                    return false;
                }
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                Product pr = await base._repo.FindAsync(id);
                if (pr != null)
                {
                    return pr;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public async Task<Product> GetProductByName(string name)
        {
            try
            {
                Product pr = await base._repo.FirstOrDefaultAsync(x => x.Name == name);
                if (pr != null)
                {
                    return pr;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public async Task<List<Product>> GetProducts(int page, int count, bool IsActive)
        {
            try
            {
                return await base._repo.Where(x => x.IsActive == IsActive)
                      .Skip(page * count).Take(count).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Product>();
                throw;
            }
        }

        public async Task<List<Product>> GetProductsByGroupId(int GroupId, bool IsActive)
        {
            try
            {
                return await base._repo.Where(x => x.GroupId == GroupId && x.IsActive == IsActive).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
                throw;
            }
        }
    }
}
