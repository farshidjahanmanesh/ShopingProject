using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.ProductServices;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ShowPost(int id)
        {
            if (id<0)
            {
                return RedirectToAction("index", "home");
            }
            try
            {
                var product = await service.FindProductAsync(id);
                if (!product.Equals(null))
                {
                    return View(product);
                }
                else
                {
                    //must redirect to error view
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}