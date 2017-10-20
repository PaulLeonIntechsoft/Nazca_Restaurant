using System.Web;
using System.Web.Optimization;

namespace Nazca_Restaurant
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Bootstrap CSS y JS
            bundles.Add(new StyleBundle("~/Bootstrap/Styles").Include(
                      "~/Content/bootstrap-4.0.0-beta/dist/css/bootstrap.min.css",
                      "~/Content/bootstrap-4.0.0-beta/dist/css/bootstrap-grid.min.css",
                      "~/Content/bootstrap-4.0.0-beta/dist/css/bootstrap-reboot.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Bootstrap/Scripts").Include(
                      "~/Content/bootstrap-4.0.0-beta/assets/js/vendor/popper.min.js",
                      "~/Content/bootstrap-4.0.0-beta/dist/js/bootstrap.min.js"
                ));

            bundles.Add(new StyleBundle("~/Main/Styles").Include(
                      "~/Content/individual/mainStyle.css",
                      "~/Content/reStyleBootstrap.css"
                ));

        }
    }
}
