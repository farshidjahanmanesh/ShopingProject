using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.ProductServices;
using System.Threading.Tasks;

namespace UI.ViewComponents
{
    public class LastProductTagsViewComponent : ViewComponent
    {
        private readonly IProductService productService;

        public LastProductTagsViewComponent(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = productService.GetBestKeywords();

            return View(result);
        }
    }
}
