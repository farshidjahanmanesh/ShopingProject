﻿using AutoMapper;
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

namespace ServiceLayer.Services.ProductServices
{
    public interface IProductService
    {
        #region product
        Task<TaskStatus> AddProductAsync(ProductDto pr);
        bool RemoveProduct(int id);
        void UpdateProduct(ProductDto product);
        Task<ProductDto> FindProductAsync(int id);
        List<ProductDto> FindProducts(int count, int page, string name);
        List<FeaturedProductDto> FindProductsWithTagName(int count, int page, string tagname);
        #endregion

        #region category
        List<NavbarDto> GetNavbarItems();
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
                .Include(x => x.Images).Include(x => x.Keywords).Include(x => x.Comments).Include(x => x.Details).FirstOrDefaultAsync();
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
                .Where(x => (name==null||name=="")|| x.Name.Contains(name))
                .SkipAndTake(page, count, QueryExtension.ProductOrder.date));
        }
        public List<FeaturedProductDto> FindProductsWithTagName(int count, int page, string tagname)
        {
           var tagProducts= ctx.ProductKeyword.Where(x => (tagname == "" || tagname == null) || x.Text == tagname)
                .SkipAndTake(page, count, QueryExtension.OrderBy.date);

            var DtoProducts = mapper.Map<List<FeaturedProductDto>>(tagProducts);
            DtoProducts.ForEach(x => x.TagName = tagname );
            return DtoProducts;
        }
        #endregion

        #region category
        public List<NavbarDto> GetNavbarItems()
        {
            var groups=ctx.Category.Include(x => x.Groups);
            var result = mapper.Map<List<NavbarDto>>(groups);
            return result;
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