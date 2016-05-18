using System.Web;
using System.Web.Optimization;

namespace CarSharing.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Knockout").Include(
                        "~/Scripts/Knockout-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.0.2.js",
                         "~/Scripts/jquery.validate.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
              "~/Content/site.css"));
            


        }
    }
}