using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Security.Application;
using ServiceLayer.Services.ProductServices;
using UI.Utilities;
using UI.ViewModels;

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

        public IActionResult ProductSearch(string keyword)
        {
            try
            {
                keyword = Sanitizer.GetSafeHtmlFragment(keyword);
                if (keyword.Equals("") || keyword.Equals(null))
                {
                    //need to redirect to error page
                    return null;

                }
                var result = service.FindProductsWithTagName(20, 0, keyword);
               
                return View(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}