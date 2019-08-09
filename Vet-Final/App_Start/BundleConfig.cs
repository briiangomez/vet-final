using System.Web;
using System.Web.Optimization;

namespace Vet_Final
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/js/modernizr").Include(
            "~/Content/js/modernizr-2.6.2.js",
            "~/Content/js/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/Styles").Include(
                      "~/Content/css/font-awesome/font-awesome.min.css",
                      "~/Content/css/jquery.ui.theme.css",
                      "~/Content/css/jquery.ui.autocomplete.min.css",
                      "~/Content/css/style.css"));

            bundles.Add(new ScriptBundle("~/Content/JavaScripts").Include(
                      "~/Content/js/jquery.validate.min.js",
                      "~/Content/js/jquery.validate.unobtrusive.min.js",
                      "~/Content/js/jquery-ui.min.js",
                      "~/Content/js/funciones.js"));
        }
    }
}
