using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.BlogServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.ViewComponents
{

    public class LastPostsViewComponent : ViewComponent
    {
        private readonly IBlogService service;
        private readonly IMapper mapper;

        public LastPostsViewComponent(IBlogService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var posts=service.FindPosts(5, 0,null);
           var result= mapper.Map<List<PostSummeryViewModel>>(posts);
            return View(result);
        }
    }
}
