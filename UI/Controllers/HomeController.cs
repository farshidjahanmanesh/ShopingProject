using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CEWorld.Convert;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.ProductServices;
using UI.Utilities;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IProductService service)
        {
            service.GetBestKeywords();
        }
        public IActionResult Index()
        {
            return View();
        }

        
    }
}