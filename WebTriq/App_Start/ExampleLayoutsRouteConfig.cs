namespace BootstrapMvcSample
{
    using System;
    using System.Web.Routing;
    using BootstrapMvcSample.Controllers;
    using NavigationRoutes;

    using WebTriq.Controllers;

    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index(), "home");
            routes.MapNavigationRoute<HomeController>("Concept", c => c.Concept(), "home");
            routes.MapNavigationRoute<HomeController>("FAQ", c => c.Faq(), "home");
            routes.MapNavigationRoute<CalculatorController>("Calculator", c => c.Index());
            routes.MapNavigationRoute<HomeController>("Blog", c => c.Blog(), "home");
            routes.MapNavigationRoute<HomeController>("Sample", c => c.Sample(), "home");
            routes.MapNavigationRoute<HomeController>("Survey", c => c.Survey(), "home");
            routes.MapNavigationRoute<HomeController>("Forums", c => c.Forums(), "home");
            routes.MapNavigationRoute<HomeController>("Legal", c => c.Legal(), "home");
        }
    }
}
