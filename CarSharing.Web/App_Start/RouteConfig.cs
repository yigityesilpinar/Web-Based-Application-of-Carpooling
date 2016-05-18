using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarSharing.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{area}/{controller}/{action}/{id}",
                defaults: new { area = "Main", controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "CarSharing.Web.Areas.Main.Controllers" }// Parameter defaults
            ).DataTokens.Add("area", "Main");
        }
    }
}