using AutoMapper;
using EntityModels.DTOs;
using EntityModels.Entities.Posts;
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
                cfg.CreateMap<PostDto, Post>().MapOnlyIfChanged();
                cfg.CreateMap<Post, PostDto>().MapOnlyIfChanged();
            });
        }

        IMapper MyMapper.CreateMappings()
        {
            return config.CreateMapper();
        }



    }
}
