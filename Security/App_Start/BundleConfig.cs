using System.Web;
using System.Web.Optimization;

namespace Security
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-2.1.3.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/additional-methods.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include("~/Scripts/jquery.dataTables.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/angular.js", "~/Scripts/angular-route.js", "~/js/Module.js"));

            //------------------------bootstrap 3.3.1----------------------------
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/ie10-viewport-bug-workaround.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-2.7.2.js"));

            //------------------------adding style css------------------------------
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include("~/Content/bootstrap/css/bootstrap.css", "~/Content/bootstrap/css/bootstrap-theme.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap/signin").Include("~/Content/bootstrap/css/signin.css"));

            bundles.Add(new StyleBundle("~/Content/dataTables").Include("~/Content/jquery.dataTables.css"));
        }
    }
}