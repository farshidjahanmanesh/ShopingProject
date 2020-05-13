using DataAccess.Context;
using EntityModels.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Services.SiteServices
{
    public interface ISiteService
    {
        #region slider
        List<Slider> GetSliders();
        #endregion


        #region newsletter
        bool AddNewsLetter(string Email);
        #endregion


        bool SaveChanges();
    }
    public class SiteService : ISiteService
    {
        private readonly ShopDbContext ctx;

        public SiteService(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }



        #region slider
        public List<Slider> GetSliders()
        {
            return ctx.Slider.ToList();
        }
        #endregion

        #region newsletter
        public bool AddNewsLetter(string Email)
        {
            if (Email.Equals(null) || Email.Equals(""))
            {
                return false;
            }

            if (ctx.Newsletter.Any(x => x.Email == Email))
            {
                return false;
            }

            ctx.Newsletter.Add(new NewsLetter()
            {
                Email = Email
            });
            return true;
        }
        #endregion

        public bool SaveChanges()
        {
            ctx.SaveChanges();
            return true;
        }


    }
}
