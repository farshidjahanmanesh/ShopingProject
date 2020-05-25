using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityModels.Entities.Site;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Security.Application;
using ServiceLayer.Services.SiteServices;
using UI.Utilities;

namespace UI.Controllers
{
    public class BaseWorkController : Controller
    {
        private readonly ISiteService service;

        public BaseWorkController(ISiteService service)
        {
            this.service = service;
        }

        [HttpPost]
        public JsonResult AddNewsLetter([FromBody]NewsLetter newsletter)
        {

            if (!newsletter.Email.EmailValidation())
            {
                return Json("Email Is Not Valid");
            }

            newsletter.Email = Sanitizer.GetSafeHtmlFragment(newsletter.Email);
            newsletter.Email=newsletter.Email.Trim();
            if (newsletter.Email.Equals(null) || newsletter.Email.Equals(""))
            {
                return Json("reject");
            }
            bool result = service.AddNewsLetter(newsletter.Email);
            service.SaveChanges();
            return result ? Json("accept") : Json("reject");
        }

        
    }
}