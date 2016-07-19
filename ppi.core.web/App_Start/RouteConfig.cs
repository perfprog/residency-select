using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PPI.Core.Web
{
    public class RouteConfig
    {
        [Log]
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Ignore("Scripts/");
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
                name: "Error404",
                url: "Error404/{action}/{id}",
                defaults: new { controller = "Error", action = "Error404", id = UrlParameter.Optional }
            );            

            routes.MapRoute(
                name: "Administration",
                url: "Administration/{controller}/{action}/{id}",
                defaults: new {controller = "Administration", action="Index",id= UrlParameter.Optional},
                constraints: new {controller = "Hogan|People|Reports|Site|Program"  }

            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }                
            );
              
        }
    }
}
