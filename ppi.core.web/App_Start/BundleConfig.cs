using System.Web;
using System.Web.Optimization;

namespace PPI.Core.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        [Log]
        public static void RegisterBundles(BundleCollection bundles)
        {            
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery.unobtrusive-ajax.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/bootstraptheme").Include(
                      "~/Scripts/Theme/scripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Libraries/datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/csstheme").Include(
                      "~/Content/Libraries/font-awesome.css",
                      "~/Content/Libraries/nanoscroller.css",
                      "~/Content/Template/layout.css",
                      "~/Content/Template/elements.css"));

            bundles.Add(new ScriptBundle("~/bundles/spin").Include(
                      "~/Scripts/spin.js"));

            bundles.Add(new ScriptBundle("~/bundles/themeplugin").Include(
                      "~/Scripts/Plugin/jquery.nanoscroller.min.js"));
                      
            bundles.Add(new ScriptBundle("~/bundles/Datetpicker").Include(
                       "~/Scripts/Plugin/bootstrap-datepicker.js"));
        }
    }
}
