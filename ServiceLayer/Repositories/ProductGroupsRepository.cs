using DataAccess.UnitOfWork;
using EntityModels.Entities.Categories;
using EntityModels.Entities.Products;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories
{
    public interface IProductGroupsRepository : IBaseRepository<ProductGroups>
    {
        Task<List<Product>> GetGroupProductsAsync(int id, bool IsActive);
        Task<List<Product>> GetGroupProductsAsync(ProductGroups pr, bool IsActive);
        Task<List<ProductGroups>> GetProductGroupsByCategoryId(int categoryId, bool IsActive);
    }

    public class ProductGroupsRepository : BaseRepository<ProductGroups>, IProductGroupsRepository
    {
        //base repo in this Repository is a instance of category DbSet


        //private readonly DbContext _ctx;
        private readonly DbSet<Category> _categories;
        private readonly IProductRepository _productRepository;
        public ProductGroupsRepository(IUnitOfWork UW) : base(UW)
        {
            //this._ctx = _ctx;
            this._categories = UW.DB.Set<Category>();
            this._productRepository = UW.productRepository;
        }
        public async override Task<bool> AddAsync(ProductGroups pr)
        {
            try
            {
                if (pr.CategoryId == 0)
                {
                    return false;
                }
                var Category = await _categories.AnyAsync(x => x.Id == pr.CategoryId);
                if (Category == false)
                {
                    return false;
                }
                return await base.AddAsync(pr);
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public async Task<List<Product>> GetGroupProductsAsync(int id, bool IsActive)
        {
            try
            {

                var Result = await _repo.FindAsync(id);
                if (Result!=null)
                {
                    return await GetGroupProductsAsync(Result,IsActive);
                }
                else
                {
                    return new List<Product>();
                }


            }
            catch (Exception)
            {
                return new List<Product>();
                throw;
            }
        }

        public async Task<List<Product>> GetGroupProductsAsync(ProductGroups pr, bool IsActive)
        {
            try
            {
                if (pr != null)
                {
                    return await _productRepository.GetProductsByGroupId(pr.Id, IsActive);
                }
                else
                {
                    return new List<Product>();
                }
            }
            catch (Exception)
            {
                return new List<Product>();
                throw;
            }
        }

        public async Task<List<ProductGroups>> GetProductGroupsByCategoryId(int categoryId,bool IsActive)
        {
            try
            {
                var items = await base._repo.Where(x => x.CategoryId == categoryId && x.IsActive == IsActive).ToListAsync();
                if (items == null)
                {
                    items = new List<ProductGroups>();
                }
                return items;
            }
            catch (Exception ex)
            {
                return new List<ProductGroups>();
                throw;
            }
        }
    }

}
