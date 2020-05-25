using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.Services.ProductServices;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.ViewComponents
{
    public class ShopCartViewComponent : ViewComponent
    {
        private readonly IProductService service;
        private readonly IMapper mapper;

        public class ListShopingCart
        {
            public int count { get; set; }
            public int TotalPrice { get; set; }
            public List<CartProductViewModel> Products { get; set; } = new List<CartProductViewModel>();
        }
        public ShopCartViewComponent(IProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ListShopingCart ShopCart = new ListShopingCart();



            if (HttpContext.Request.Cookies.Count > 0)
            {
                var JsonResult = Request.Cookies["ShopCart"];
                Dictionary<int, int> result=new Dictionary<int, int>();
                if (JsonResult != null)
                {
                     result = JsonConvert.DeserializeObject<Dictionary<int, int>>(JsonResult);
                }
               

                foreach (var item in result)
                {
                    var resultItem = await service.FindProductAsyncWithOutDependencies(item.Key);
                    if (resultItem == null)
                    {
                        continue;
                    }
                    var viewModelItem = mapper.Map<CartProductViewModel>(resultItem);

                    viewModelItem.CountBuy = item.Value;
                    if (item.Value <= 0)
                    {
                        viewModelItem.CountBuy = 1;
                    }
                    ShopCart.Products.Add(viewModelItem);
                    ShopCart.count += item.Value > 0 ? item.Value : 1;
                    ShopCart.TotalPrice += ((int)resultItem.Price * item.Value);

                }
            }

            return View(ShopCart);
        }
    }
}
