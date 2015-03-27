namespace WebTriq
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Canonicalize;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Canonicalize()
                  .Lowercase()
                  .NoTrailingSlash();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Sitemap", 
                "sitemap.xml", 
                new { controller = "Home", action = "Sitemap", id = UrlParameter.Optional });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}