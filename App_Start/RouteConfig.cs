using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLBanVePhim
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            name: "MovieList",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional }
        );

            routes.MapRoute(
                name: "testView",
                url: "{controller}/{action}",
                defaults: new { controller = "QLHome", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "QLKH",
               url: "{controller}/{action}",
               defaults: new { controller = "QLHome", action = "QLKH", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Login",
               url: "{controller}",
               defaults: new { controller = "LoginQL", action = "Index", id = UrlParameter.Optional }
           );

           
        }
    }
}
