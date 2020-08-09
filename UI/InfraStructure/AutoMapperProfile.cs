using AutoMapper;
using EntityModels.DTOs.CategoriesDtos;
using EntityModels.DTOs.PostDtos;
using EntityModels.DTOs.ProductDtos;
using EntityModels.DTOs.SiteDtos;
using EntityModels.Entities.Products;
using EntityModels.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.InfraStructure
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDto, CartProductViewModel>();
            CreateMap<ProductDto, NewProductsViewModel>();
            CreateMap<FeaturedProductDto, FeaturedProductViewModel>();
            CreateMap<PostSummeryDto, PostSummeryViewModel>();
            CreateMap<SummeryLastCommentDto, LastCommentsViewModel>();
            CreateMap<NavbarDto, NavbarViewModel>();
            CreateMap<NavbarDto.NestedProductGroupDto, NavbarViewModel.NestedProductViewModel>();
            CreateMap<SliderDto, SliderViewModel>();
            CreateMap<PostDto, ShowSinglePostViewModel>();

        }
    }
}
