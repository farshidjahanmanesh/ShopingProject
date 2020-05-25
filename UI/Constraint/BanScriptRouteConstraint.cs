using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Constraint
{
    public class BanScriptRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route,
            string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {

            var RouteValue = values[routeKey].ToString();

            string Safevalue = Sanitizer.GetSafeHtmlFragment(RouteValue);
            
            return Safevalue == RouteValue ? true : false;
            
            throw new NotImplementedException();
        }
    }
}
