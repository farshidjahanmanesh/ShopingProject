using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Services.ProductServices;
using StackExchange.Profiling;
using UI.Utilities;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(IProductService serivce, ILogger<HomeController> logger)
        {
            this.logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                logger.LogInformation("first log");
                using (MiniProfiler.Current.Step("InitUser"))
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return null;
            }
            

        }

        public IActionResult Error()
        {
            return null;
        }


    }
}