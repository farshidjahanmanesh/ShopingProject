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
    }
    public class SiteService:ISiteService
    {
        private readonly ShopDbContext ctx;

        public SiteService(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }

        public List<Slider> GetSliders()
        {
            return ctx.Slider.ToList();
        }

        #region slider

        #endregion

    }
}
