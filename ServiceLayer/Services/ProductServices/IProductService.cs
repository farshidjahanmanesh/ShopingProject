using AutoMapper;
using DataAccess.Context;
using EntityModels.DTOs.CategoriesDtos;
using EntityModels.DTOs.ProductDtos;
using EntityModels.Entities.Products;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.CheckDtoValidations;
using ServiceLayer.QueryExtenstion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.QueryExtenstion.QueryExtension;

namespace ServiceLayer.Services.ProductServices
{
    public interface IProductService
    {
        #region product
         Task<ProductDto> FindProductAsyncWithOutDependencies(int id);
        Task<TaskStatus> AddProductAsync(ProductDto pr);
        bool RemoveProduct(int id);
        void UpdateProduct(ProductDto product);
        Task<ProductDto> FindProductAsync(int id);
        List<ProductDto> FindProducts(int count, int page, string name);
        List<FeaturedProductDto> FindProductsWithTagName(int count, int page, string tagname, ProductOrder orderby = ProductOrder.dateDes);
        int TotalProductByKeywordCount(string tagname);


        #endregion

        #region category
        public int ProductGroupCount(int groupid);
        public List<FeaturedProductDto> FindProductWithGroupId(int groupId, int page, int count, ProductOrder orderby = ProductOrder.dateDes);
        List<NavbarDto> GetNavbarItems();
        #endregion

        #region keyword
        Dictionary<string, int> GetBestKeywords();
        #endregion

        #region Comment
        bool AddComment(ProductCommentDto comment);
        #endregion

        #region base
        Task<int> SaveChangesAsync();
        int SaveChanges();
        #endregion
    }

    public class ProductService : IProductService
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public ProductService(ShopDbContext ctx, MyMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper.CreateMappings();
        }

        #region product
        public int TotalProductByKeywordCount(string tagname)
        {
            var ProductCount = ctx.ProductKeyword
                .Where(x => (tagname == "" || tagname == null) || x.Text == tagname)
                    .Count();
            return ProductCount;
        }
        public async Task<TaskStatus> AddProductAsync(ProductDto pr)
        {
            if (!CheckValidation.CheckObjectIsValid<ProductDto>(pr))
            {
                return TaskStatus.Faulted;
            }
            var mapSource = mapper.Map<Product>(pr);
            await ctx.Product.AddAsync(mapSource);
            return TaskStatus.RanToCompletion;
        }
        public bool RemoveProduct(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            ctx.Product.Remove(new Product()
            {
                Id = id
            });
            return true;
        }

        public async Task<ProductDto> FindProductAsync(int id)
        {
            var product = await ctx.Product.Where(x => x.Id == id)
                .Include(x => x.Images).Include(x => x.Keywords)
                .Include(x => x.Comments).Include(x => x.Details)
                .Include(x => x.Group).ThenInclude(x => x.Category).FirstOrDefaultAsync();
            return mapper.Map<ProductDto>(product);
        }
        public async Task<ProductDto> FindProductAsyncWithOutDependencies(int id)
        {
            var product = await ctx.Product.Where(x => x.Id == id).FirstOrDefaultAsync();
            return mapper.Map<ProductDto>(product);
        }

        public void UpdateProduct(ProductDto product)
        {
            var baseproduct = ctx.Product.Find(product.Id);
            baseproduct = (Product)mapper.Map(product, baseproduct, typeof(ProductDto), typeof(Product));
        }

        public List<ProductDto> FindProducts(int count, int page, string name)
        {
            return mapper.Map<List<ProductDto>>(ctx.Product.AsNoTracking()
                .Where(x => (name == null || name == "") || x.Name.Contains(name))
                .SkipAndTake(page, count, QueryExtension.ProductOrder.dateDes));
        }
        public List<FeaturedProductDto> FindProductsWithTagName(int count, int page, string tagname, ProductOrder orderby = ProductOrder.dateDes)
        {
            var tagProducts = ctx.ProductKeyword.AsNoTracking()
                .Where(x => (tagname == "" || tagname == null) || x.Text == tagname).Select(x => x.Product)
                 .SkipAndTake(page, count, orderby).ToList();

            var DtoProducts = mapper.Map<List<FeaturedProductDto>>(tagProducts);
            DtoProducts.ForEach(x => x.TagName = tagname == null ? "" : tagname);
            return DtoProducts;
        }
        #endregion

        #region category
        public List<NavbarDto> GetNavbarItems()
        {
            var groups = ctx.Category.AsNoTracking().Include(x => x.Groups);
            var result = mapper.Map<List<NavbarDto>>(groups);
            return result;
        }
        public List<FeaturedProductDto> FindProductWithGroupId(int groupId, int page, int count, ProductOrder orderby = ProductOrder.dateDes)
        {
            var QueryResult = ctx.Product.AsNoTracking().Where(x => x.GroupId == groupId)
                 .SkipAndTake(page, count, orderby).ToList();
            var DtoProducts = mapper.Map<List<FeaturedProductDto>>(QueryResult);
            var GroupName = ctx.ProductGroups.Find(groupId).Text;
            DtoProducts.ForEach(x => x.TagName = GroupName == null ? "" : GroupName);
            return DtoProducts;
        }
        public int ProductGroupCount(int groupid)
        {
            return ctx.Product.Where(x => x.GroupId == groupid).Count();
        }
        #endregion


        #region Comment
        public bool AddComment(ProductCommentDto comment)
        {
            var MapperResult = mapper.Map<ProductComment>(comment);
            ctx.ProductComment.Add(MapperResult);
            return true;
            //            ctx.ProductComment.Add(comment);

        }
        #endregion



        #region keyword
        public Dictionary<string, int> GetBestKeywords()
        {
            var res = ctx.ProductKeyword.AsNoTracking().GroupBy(x => x.Text).Take(10)
                .Select(e => new { e.Key, count = e.Count() })
                .ToDictionary(e => e.Key, e => e.count);
            return res;
        }
        #endregion


        #region base
        public int SaveChanges()
        {
            return ctx.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await ctx.SaveChangesAsync();
        }

        #endregion

    }

}
