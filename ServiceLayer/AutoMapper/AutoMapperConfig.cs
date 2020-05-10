using AutoMapper;
using EntityModels.DTOs;
using EntityModels.DTOs.CategoriesDtos;
using EntityModels.DTOs.PostDtos;
using EntityModels.DTOs.ProductDtos;
using EntityModels.Entities.Categories;
using EntityModels.Entities.Posts;
using EntityModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.AutoMapper
{
    public static class AutoMapperExtension
    {

        public static IMappingExpression<TSource, TDestination> MapOnlyIfChanged<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map)
        {
            map.ForAllMembers(source =>
            {
                source.Condition((sourceObject, destObject, sourceProperty, destProperty) =>
                {
                 
                    if (sourceProperty == null)
                    {
                        if (destProperty != null)
                        {
                            return false;
                        }
                        return !(destProperty == null);
                    }

                    //if(sourceProperty is DateTime)
                    //{
                    //    var time = (DateTime)sourceProperty ;
                    //    var result=time.Equals((DateTime)destProperty);
                    //    if (result)
                    //        return false;
                    //    return true;
                    //}

                    //if (sourceProperty is Int32 && (int)sourceProperty==0)
                    //{
                    //    if ((int)destProperty != 0)
                    //    {
                    //        return false;
                    //    }
                    //}

                    return !sourceProperty.Equals(destProperty);
                });
            });
            return map;
        }

    }
    

    public interface MyMapper
    {
       abstract IMapper CreateMappings();
    }

    public class AutoMapperConfig: MyMapper
    {
        public static MapperConfiguration config { get;private set; }

        static  AutoMapperConfig()
        {
            config = new MapperConfiguration(cfg => {
                
                cfg.CreateMap<PostSummeryDto, Post>().MapOnlyIfChanged();
                cfg.CreateMap<Post, PostSummeryDto>().MapOnlyIfChanged();
                cfg.CreateMap<PostDto, Post>().MapOnlyIfChanged();
                cfg.CreateMap<Post, PostDto>().MapOnlyIfChanged();
                cfg.CreateMap<Product, ProductDto>().MapOnlyIfChanged();
                cfg.CreateMap<ProductDto, Product>().MapOnlyIfChanged();
                cfg.CreateMap<FeaturedProductDto, ProductDto>().MapOnlyIfChanged();
                cfg.CreateMap<ProductDto, FeaturedProductDto>().MapOnlyIfChanged();
                cfg.CreateMap<Product, FeaturedProductDto>().MapOnlyIfChanged();
                cfg.CreateMap<FeaturedProductDto, Product>().MapOnlyIfChanged();
                cfg.CreateMap<PostComment, SummeryLastCommentDto>().MapOnlyIfChanged();
                cfg.CreateMap<Category, NavbarDto>().MapOnlyIfChanged();
                cfg.CreateMap<ProductGroups, NavbarDto.NestedProductGroupDto>().MapOnlyIfChanged();
                cfg.CreateMap<ProductComment, ProductCommentDto>().MapOnlyIfChanged();
                cfg.CreateMap<ProductCommentDto, ProductComment>().MapOnlyIfChanged();
            });
        }

        IMapper MyMapper.CreateMappings()
        {
            return config.CreateMapper();
        }



    }
}
