using AutoMapper;
using EntityModels.DTOs.CategoriesDtos;
using EntityModels.DTOs.PostDtos;
using EntityModels.DTOs.ProductDtos;
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
            CreateMap<ProductDto, NewProductsViewModel>();
            CreateMap<FeaturedProductDto, FeaturedProductViewModel>();
            CreateMap<PostSummeryDto, PostSummeryViewModel>();
            CreateMap<SummeryLastCommentDto, LastCommentsViewModel>();
            CreateMap<NavbarDto, NavbarViewModel>();
            CreateMap<NavbarDto.NestedProductGroupDto, NavbarViewModel.NestedProductViewModel>();
            CreateMap<Slider, SliderViewModel>();
            CreateMap<PostDto, ShowSinglePostViewModel>();

        }
    }
}
