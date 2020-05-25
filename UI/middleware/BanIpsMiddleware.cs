using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UI.middleware
{
    public class BanIpsMiddleware
    {
        private class RootJson
        {
            public RootJson()
            {
                Ips = new HashSet<IpObject>();
            }
            public HashSet<IpObject> Ips { get; set; }
        }
        private class IpObject
        {
            public string Ip { get; set; }
            public string Detail { get; set; }
        }
        private RequestDelegate _next;
        private readonly IWebHostEnvironment environment;

        public BanIpsMiddleware(RequestDelegate _next, IWebHostEnvironment environment)
        {
            this._next = _next;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            var fileroot = Path.Combine(environment.WebRootPath, "banIps.json");
           // var fileroot = Path.Combine(Directory.GetCurrentDirectory(), "banIps.json");
            var JSON = System.IO.File.ReadAllText(fileroot);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<RootJson>(JSON);
            var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
            var finalResult = result == null ? false : result.Ips.Any(x => x.Ip == remoteIpAddress);
            if (finalResult)
            {
                await context.Response.WriteAsync($"you are ban by this site for {result.Ips.First(x=>x.Ip==remoteIpAddress).Detail}");
            }
            else
            {
                await _next(context);
            }
            
        }
    }
}
