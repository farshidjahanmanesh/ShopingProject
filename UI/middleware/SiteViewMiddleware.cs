using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.middleware
{
    public class SiteViewMiddleware
    {
        private static HashSet<string> IpAddress;
        private static DateTime Date;
        private RequestDelegate _next;
        static SiteViewMiddleware()
        {
            Date = DateTime.Now;
            IpAddress = new HashSet<string>();
        }
        public SiteViewMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task Invoke(HttpContext context)
        {

            await _next(context);

            if ((DateTime.Now - Date).TotalHours > 24)
            {
                Date = DateTime.Now;
                IpAddress.Clear();
            }
            var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
            if (remoteIpAddress != null)
                if (!IpAddress.Any(x => x == remoteIpAddress))
                {
                    IpAddress.Add(remoteIpAddress);
                    //in this section you can add all ip address in IpAddress to database
                    //or add IpAddress.Count just for get count of person view your site

                }


        }


    }
}
