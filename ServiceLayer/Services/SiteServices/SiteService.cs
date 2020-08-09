using AutoMapper;
using DataAccess.Context;
using EntityModels.DTOs.SiteDtos;
using EntityModels.Entities.Site;
using ServiceLayer.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Services.SiteServices
{
    public interface ISiteService
    {
        #region slider
        List<SliderDto> GetSliders();
        #endregion


        #region newsletter
        bool AddNewsLetter(string Email);
        #endregion


        bool SaveChanges();
    }
    public class SiteService : ISiteService
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public SiteService(ShopDbContext ctx,MyMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper.CreateMappings();
        }



        #region slider
        public List<SliderDto> GetSliders()
        {
           return mapper.Map<List<SliderDto>>(ctx.Slider.ToList());
           
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
