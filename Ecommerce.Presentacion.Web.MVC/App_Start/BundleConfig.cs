using System.Web;
using System.Web.Optimization;

namespace Ecommerce.Presentacion.Web.MVC
{
    public class BundleConfig
    {
        // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js",
                        "~/Scripts/token-input/jquery.tokeninput.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jangular").Include(
                        "~/Scripts/Angular/angular.js",
                        "~/Scripts/Angular/angular-route.js",
                        "~/Scripts/Angular/ngStorage.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css", 
                "~/Content/shop-homepage.css", 
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-dialog.css"));
            bundles.Add(new StyleBundle("~/Content/token-input/css").Include("~/Content/token-input/token-input-facebook.css",
                "~/Content/token-input/token-input.css"));
        }
    }
}