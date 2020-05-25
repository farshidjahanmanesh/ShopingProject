using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels.DTOs.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Security.Application;
using Newtonsoft.Json;
using ServiceLayer.Services.ProductServices;
using UI.Utilities;
using UI.ViewModels;
using static ServiceLayer.QueryExtenstion.QueryExtension;
using static UI.ViewComponents.UIPagingViewComponent;

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

        public async Task<IActionResult> ShowProduct(int id)
        {
            if (id < 0)
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

        [HttpPost]
        public JsonResult SaveCommnet([FromBody]CommentViewModel comment, int postid)
        {
            comment.Email = Sanitizer.GetSafeHtmlFragment(comment.Email);
            comment.name = Sanitizer.GetSafeHtmlFragment(comment.name);
            comment.Text = Sanitizer.GetSafeHtmlFragment(comment.Text);
            if (ModelState.IsValid)
            {
                if (!comment.Email.EmailValidation())
                {
                    return Json("Email Is Not Valid");
                }

                service.AddComment(new EntityModels.DTOs.ProductDtos.ProductCommentDto()
                {
                    Email = comment.Email,
                    Name = comment.name,
                    ProductId = postid,
                    Text = comment.Text
                });

                service.SaveChanges();
                return Json("success");
            }
            else
            {
                StringBuilder result = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var erroritem in item.Errors)
                    {
                        result.AppendLine(erroritem.ErrorMessage);

                    }
                }
                return Json(result.ToString());
            }


        }

        public IActionResult ProductSearch(string keyword, int page = 0, int order = 0)
        {
            try
            {
                int count = 9;
                if (page < 0)
                    page = 0;

                ProductOrder porder = ProductOrder.dateDes;
                string OrderName = "تاریخ : جدیدترین";
                switch (order)
                {
                    case 0:
                        porder = ProductOrder.dateDes;
                        break;

                    case 1:
                        porder = ProductOrder.dateAs;
                        OrderName = "تاریخ : قدیمی ترین";
                        break;

                    case 2:
                        porder = ProductOrder.priceDes;
                        OrderName = "قیمت : صعودی";
                        break;

                    case 3:
                        porder = ProductOrder.priceAs;
                        OrderName = "قیمت : نزولی";
                        break;

                }

                keyword = Sanitizer.GetSafeHtmlFragment(keyword);
                if (keyword.Equals("") || keyword.Equals(null))
                {
                    //need to redirect to error page
                    return null;

                }
                var result = service.FindProductsWithTagName(count, page, keyword, porder);
                if (result == null)
                {
                    return View(new List<FeaturedProductDto>());
                }


                Pager<FeaturedProductDto> products = new Pager<FeaturedProductDto>(result, service.TotalProductByKeywordCount(keyword), count, page);

                products.Key = keyword;
                products.OrderSelect = OrderName;
                return View(products);
                // return View(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult CategoryProductSearch(int id, int page = 0, int order = 0)
        {

            try
            {
                int count = 9;
                if (page < 0)
                    page = 0;

                ProductOrder porder = ProductOrder.dateDes;
                string OrderName = "تاریخ : جدیدترین";
                switch (order)
                {
                    case 0:
                        porder = ProductOrder.dateDes;
                        break;

                    case 1:
                        porder = ProductOrder.dateAs;
                        OrderName = "تاریخ : قدیمی ترین";
                        break;

                    case 2:
                        porder = ProductOrder.priceDes;
                        OrderName = "قیمت : صعودی";
                        break;

                    case 3:
                        porder = ProductOrder.priceAs;
                        OrderName = "قیمت : نزولی";
                        break;

                }

                var result = service.FindProductWithGroupId(id, page, count, porder);
                if (result == null)
                {
                    return View(new List<FeaturedProductDto>());
                }


                Pager<FeaturedProductDto> products = new Pager<FeaturedProductDto>(result, service.ProductGroupCount(id), count, page);

                products.Key = id.ToString();
                products.OrderSelect = OrderName;
                return View(products);
                // return View(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult RemoveFromCookie([FromBody]int id)
        {
            var json = Request.Cookies["shopcart"];
            var result = JsonConvert.DeserializeObject<Dictionary<int, int>>(json);
            if (result.Any(x => x.Key == id))
            {
                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30)
                };
                result.Remove(id);
                Response.Cookies.Append("shopcart", JsonConvert.SerializeObject(result), options);
                return Json("remove");
            }
            else
            {
                return Json("not valid");
            }
        }

        [HttpPost]
        public JsonResult AddToCoockie([FromBody]ProductCockieViewModel pr)
        {
            try
            {
                if (pr == null)
                {
                    return Json("false");
                }
                pr.Count = Sanitizer.GetSafeHtmlFragment(pr.Count);
                pr.Productid = Sanitizer.GetSafeHtmlFragment(pr.Productid);

                int count = Int32.Parse(pr.Count);
                int id = Int32.Parse(pr.Productid);
                if (id <= 0 || count <= 0)
                {
                    return Json("false");
                }

                var options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30)
                };
                var Jsonresult = Request.Cookies["shopcart"];
                Dictionary<int, int> result;
                if (Jsonresult != null)
                {
                     result = JsonConvert.DeserializeObject<Dictionary<int, int>>(Jsonresult);
                }
                else
                {
                    result = new Dictionary<int, int>();
                }

                if (result.Any(x => x.Key == id))
                {
                    result[id] += count;
                }
                else
                {
                    result.Add(id, count);
                }

                Response.Cookies.Append("shopcart", JsonConvert.SerializeObject(result)
                    , options);

                return Json("add");
            }
            catch (Exception ex)
            {
                return Json("false");
            }
            

            
           
        }
        public IActionResult ShopCartRefresh()
        {
            return ViewComponent("ShopCart");
        }


        
    }
}