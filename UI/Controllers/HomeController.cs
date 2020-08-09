using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Services.ProductServices;
using StackExchange.Profiling;
using UI.Utilities;

namespace UI.Controllers
{

    public class test
    {
        [att]
        public int MyProperty { get; set; }
    }

    public class att : Attribute, IModelValidator
    {
        public att()
        {

        }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var dbcontext=(ShopDbContext)context.ActionContext.HttpContext
                .RequestServices.GetService(typeof(ShopDbContext));
            return null;
        }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
            

        }
        public IActionResult Index()
        {
            var z=(ShopDbContext)HttpContext.RequestServices.GetService(typeof(ShopDbContext));
            var pr=z.Product.Find(174);
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error in  /home/index Route ");
                return RedirectToAction(actionName: "error500", controllerName: "error");
            }


        }

        public IActionResult Error()
        {
            return null;
        }


    }
}