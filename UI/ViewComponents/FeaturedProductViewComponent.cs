using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AutoMapper;
using ServiceLayer.Services.ProductServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.ViewComponents
{
    public class FeaturedProductViewComponent : ViewComponent
    {
        private readonly IProductService service;
        private readonly IMapper mapper;
        public FeaturedProductViewComponent(IProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var ProductDto = service.FindProductsWithTagName(8, 0, "ویژه");
            var result = mapper.Map<List<FeaturedProductViewModel>>(ProductDto);
            return View(result);
        }
    }
}
