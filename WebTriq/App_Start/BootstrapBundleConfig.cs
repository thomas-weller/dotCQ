namespace BootstrapSupport
{
    using System.Web.Optimization;

    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                   .Include(
                        "~/Scripts/disclaimer.js",
                        "~/Scripts/jquery-1.9.1.js",
                        "~/Scripts/triq/triq.js",
                        "~/Scripts/triq/bootstrap.js",
                        "~/Scripts/triq/bootstrap.min.js",
                        "~/Scripts/triq/jquery.localscroll-1.2.7-min.js",
                        "~/Scripts/triq/jquery.scrollto-1.4.3.1-min.js",
                        "~/Scripts/triq/jquery-easing.js",
                        "~/Scripts/triq/quicksand.js",
                        "~/Scripts/jquery-migrate-{version}.js",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/jquery.validate.js",
                        "~/scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                        "~/Scripts/jquery.tableofcontents.js",
                        "~/Scripts/jquery.tableofcontents.min.js",
                        "~/Scripts/sharing.js"));

            bundles.Add(new StyleBundle("~/content/css")
                   .Include(
                        "~/Content/layout.css",
                        "~/Content/triq/css/bootstrap.css",
                        "~/Content/triq/css/bootstrap.min.css",
                        "~/Content/triq/css/demo.css",
                        "~/Content/triq/css/triq.css",
                        "~/Content/triq/css/websymbols.css"));
        }
    }
}