using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EntityModels.DTOs.PostDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Security.Application;
using ServiceLayer.Services.BlogServices;
using UI.Utilities;
using UI.ViewModels;

namespace UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService service;
        private readonly IMapper mapper;

        public BlogController(IBlogService service,IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        
        public async Task<JsonResult> SaveComment([FromBody] CommentViewModel comment,int postid)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    comment.Text = Sanitizer.GetSafeHtmlFragment(comment.Text);
                    comment.Email = Sanitizer.GetSafeHtmlFragment(comment.Email);
                    comment.name = Sanitizer.GetSafeHtmlFragment(comment.name);

                    if (!comment.Email.EmailValidation())
                    {
                        return Json("email is not valid");
                    }
                    await service.AddCommentAsync(new PostCommentDto()
                    {
                        Email = comment.Email,
                        Name = comment.name,
                        Text = comment.Text,
                        PostId = postid
                    });
                    await service.SaveChangesAsync();
                    return Json("success");
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in ModelState.Values)
                    {
                        foreach (var error in item.Errors)
                        {
                            sb.AppendLine(error.ErrorMessage);
                        }
                    }
                    return Json(sb.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public async Task<IActionResult> GetPost(int PostId)
        {
            try
            {
                if (PostId < 1)
                {
                    return RedirectToAction("index", "home");
                }

                var PostResult = await service.FindPostAsync(PostId);
                var result = mapper.Map<ShowSinglePostViewModel>(PostResult);
                return View(result);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}