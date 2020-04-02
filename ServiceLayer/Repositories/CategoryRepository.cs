using DataAccess.UnitOfWork;
using EntityModels.Entities.Categories;
using EntityModels.Entities.Products;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Repositories.BaseRepository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<ProductGroups>> GetProductGroupsByCategoryId(int id, bool IsActive);
    }

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        //base repo in this Repository is a instance of category DbSet
        //private readonly DbContext _ctx;
        private readonly IProductGroupsRepository productGroupsRepository;

        public CategoryRepository(IUnitOfWork UW) : base(UW)
        {
            //this._ctx = _ctx;
            this.productGroupsRepository = UW.productGroupsRepository;
        }

        public async Task<List<ProductGroups>> GetProductGroupsByCategoryId(int id,bool IsActive)
        {
            try
            {
                var category = await base._repo.FindAsync(id);
                if (category != null)
                {
                   return await productGroupsRepository.GetProductGroupsByCategoryId(id,IsActive);
                }
                else
                {
                    return new List<ProductGroups>();
                }
            }
            catch (System.Exception)
            {
                return new List<ProductGroups>();
                throw;
            }
        }
    }




}
