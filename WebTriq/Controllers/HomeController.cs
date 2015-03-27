namespace BootstrapMvcSample.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Xml.Linq;

    using MailChimp;

    using WebTriq.Controllers;

    public class HomeController : ZeroController
    {
        // GET
        public ActionResult Index()
        {
            const string Meta =
                ".CQ is a cloud-based web service for analyzing C# source code. It is based on well established methods and makes " +
                "code quality measuring and monitoring easy-to-use and affordable. Furthermore, it provides a way of correlating custom " +
                "company data with these findings, thus making the service as valuable and useful for accounting as it is to software developers.";

            ViewBag.Title = "Home - .CQ";
            ViewBag.MetaDescription = Meta;
            ViewBag.DisqusName = "Home";

            return View();
        }

        // GET
        public ActionResult Sample()
        {
            const string Meta =
                "This is a preview sample of a .CQ code quality report about a fictitious project. " + 
                "It is based on data gained from an open source project, combined with some faked company data.";

            ViewBag.Title = "Sample Report - .CQ";
            ViewBag.MetaDescription = Meta;
            ViewBag.DisqusName = "Report";

            return View();
        }

        // GET: FAQ
        public ActionResult Legal()
        {
            ViewBag.Title = "Legal and Company information - .CQ";
            ViewBag.DisqusName = "Legal";

            return View();
        }

        public ActionResult Faq()
        {
            const string Meta = 
                "Frequently asked questions about the .CQ web service. Answers " + 
                "some questions about what it is and how it works.";

            ViewBag.Title = "FAQ - .CQ";
            ViewBag.MetaDescription = Meta;
            ViewBag.DisqusName = "FAQ";

            return View();
        }

        // GET: Survey
        public ActionResult Survey()
        {
            const string Meta =
                "This quick survey is intended to collect some general data about the people that are interested in the .CQ service, " + 
                "and about the environment they're working in. These data will influence the direction of the further development and " + 
                "design of .CQ.";

            ViewBag.Title = "Survey - .CQ";
            ViewBag.MetaDescription = Meta;
            ViewBag.DisqusName = "Survey";

            return View();
        }

        // GET: Forums
        public ActionResult Forums()
        {
            return this.RedirectPermanent("https://dotcq.zendesk.com/forums/");
        }

        // GET: Blog
        public ActionResult Blog()
        {
            return this.RedirectPermanent("http://www.dotcq.com/blog");
        }

        // GET: Blog
        public ActionResult Css()
        {
            return View();
        }

        // GET: Concept
        public ActionResult Concept()
        {
            const string Meta =
                "This page outlines the practical and theoretical considerations behind the .CQ web service, providing details about " + 
                "the ways in which it can improve software maintainability and thus raise a software project's ROI in practice.";

            ViewBag.Title = "Concept and general considerations - .CQ";
            ViewBag.MetaDescription = Meta;
            ViewBag.DisqusName = "Concept";

            return View();
        }

        [OutputCache(Duration = 3600)]
        public ContentResult Sitemap()
        {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            var root = new XElement(ns + "urlset");

            var urls = new Dictionary<string, string>
                                {
                                    { GetUrl("Home", "Index"), "daily" },
                                    { GetUrl("Home", "Action1"), "always" },
                                    { GetUrl("AnotherController", "Index"), "weekly" },
                                    { "http://www.static-url.com/blog/", "daily" },
                                    { GetUrl("Home", "Action2"), "never" },
                                };

            foreach (KeyValuePair<string, string> kvp in urls)
            {
                root.Add(new XElement(
                                ns + "url",
                                new XElement(ns + "loc", kvp.Key),
                                new XElement(ns + "changefreq", kvp.Value)));
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    root.Save(writer);
                }

                return Content(Encoding.UTF8.GetString(stream.ToArray()), "text/xml", Encoding.UTF8);
            }
        }

        // Helper method to create full URL from controller/action name
        private string GetUrl(string controller, string action)
        {
            var values = new RouteValueDictionary(new { controller, action });
            var context = new RequestContext(HttpContext, RouteData);

            string url = RouteTable.Routes.GetVirtualPath(context, values).VirtualPath;

            return new Uri(Request.Url, url).AbsoluteUri;
        }

        [HttpPost]
        public bool Subscribe()
        {
            try
            {
                string eMailAddress = new StreamReader(Request.InputStream).ReadToEnd();

                return SubscribeToMailChimpList(eMailAddress);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static bool SubscribeToMailChimpList(string eMailAddress)
        {
            var mc = new MCApi(ConfigurationManager.AppSettings["mailchimp.apikey"], true);

            return mc.ListSubscribe(ConfigurationManager.AppSettings["mailchimp.listid"], eMailAddress);
        }

    }
}
