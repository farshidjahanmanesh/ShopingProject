using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.SiteServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISiteService service;
        private readonly IMapper mapper;

        public SliderViewComponent(ISiteService service,IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var sliders = service.GetSliders();
            var result = mapper.Map<List<SliderViewModel>>(sliders);
            return View(result);
        }
    }
}
