using System.Web;
using System.Web.Optimization;

namespace IoC_Demo3
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/angular.min.js",
                    "~/Scripts/angular-messages.min.js",
                    "~/Scripts/angular-ui-router.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/crypto").Include(
                    "~/CryptJs/System.debug.js",
                    "~/CryptJs/System.IO.debug.js",
                    "~/CryptJs/System.Text.debug.js",
                    "~/CryptJs/System.Convert.debug.js",
                    "~/CryptJs/System.BitConverter.debug.js",
                    "~/CryptJs/System.BigInt.debug.js",
                    "~/CryptJs/System.Security.Cryptography.SHA1.debug.js",
                    "~/CryptJs/System.Security.Cryptography.debug.js",
                    "~/CryptJs/System.Security.Cryptography.RSA.debug.js"
                ));
        }
    }
}
