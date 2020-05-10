using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.ViewComponents
{

    public class NewproductsViewComponent : ViewComponent
    {
        private readonly IProductService service;
        private readonly IMapper mapper;

        public NewproductsViewComponent(IProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var products = service.FindProducts(10, 0, null);
            if (products.Equals(null))
            {
               throw new NotImplementedException();
#pragma warning disable CS0162 // Unreachable code detected
                return null;
#pragma warning restore CS0162 // Unreachable code detected
            }
              var Result=  mapper.Map<List<NewProductsViewModel>>(products);

            return View(Result);
        }
    }
}
