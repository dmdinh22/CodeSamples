using System.Web;
using System.Web.Optimization;

namespace aic.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/public-assets/plugins/jquery-1.12.3.min.js",
                         "~/public-assets/plugins/jquery.validate.min.js",
                          "~/public-assets/plugins/jquery-placeholder/jquery.placeholder.js",
                          "~/ckeditor/ckeditor.js"));

            // Admin theme jquery
            bundles.Add(new ScriptBundle("~/bundles/admin-assets/jquery").Include(
                        "~/admin-assets/jquery-3.1.1.min.js",
                        "~/public-assets/js/main.js",
                        "~/public-assets/plugins/jquery-placeholder/jquery.placeholder.js",
                        "~/ckeditor/ckeditor.js"));

            // render this bundle in your view if you need to use one of the plugins below
            bundles.Add(new ScriptBundle("~/bundles/jquery/plugins").Include(
                          "~/public-assets/plugins/FitVids/jquery.fitvids.js",
                          "~/public-assets/plugins/flexslider/jquery.flexslider.js",
                          "~/public-assets/plugins/isMobile/isMobile.js",
                          "~/public-assets/plugins/isMobile/isMobile.min.js",
                          "~/public-assets/plugins/jquery-inview/jquery.inview.js",
                          "~/public-assets/plugins/jquery-inview/jquery.inview.min.js",
                          "~/public-assets/plugins/jqueryplace/jquery.inview.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/admin-assets/bootstrap").Include(
                      "~/admin-assets/bootstrap.min.js",
                      "~/bower_components/bootstrap/js/affix.js"
                      ));

            // Inspinia script for Admin theme
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/admin-assets/inspinia.js"));

            // SlimScroll for Admin theme
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/admin-assets/plugins/slimScroll/jquery.slimscroll.min.js"));

            // jQuery plugins for Admin theme
            bundles.Add(new ScriptBundle("~/plugins/metisMenu").Include(
                      "~/admin-assets/plugins/metisMenu/metisMenu.min.js"));

            // pace plugin for admin theme
            bundles.Add(new ScriptBundle("~/plugins/pace").Include(
                      "~/admin-assets/plugins/pace/pace.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/angular-material.css"));

            // Tempo Theme Add
            bundles.Add(new StyleBundle("~/public-assets/css").Include(
                    "~/styles.css",
                    "~/styles-2.css",
                    "~/styles-3.css",
                    "~/styles-4.css",
                    "~/styles-5.css",
                    "~/styles-6.css",
                    "~/styles-7.css",
                    "~/styles-8.css",
                    "~/styles-9.css",
                    "~/styles-10.css"));

            // Admin Theme Add css
            bundles.Add(new StyleBundle("~/admin-assets/css").Include(
          "~/admin-assets/bootstrap.min.css",
          "~/admin-assets/animate.css",
          "~/admin-assets/style.css"));

            // Font Awesome icons for Admin theme
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/admin-assets/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));


            //  aic application
            //  ------------------------------------------------------------
            bundles.Add(new ScriptBundle("~/aic/base").Include(
                      "~/Scripts/aic.js"));


            //  aic application
            //  ------------------------------------------------------------
            bundles.Add(new ScriptBundle("~/aic/core").Include(

                      "~/Scripts/aic/aic.module.js",
                      "~/Scripts/aic/core/controllers/*.js",
                      "~/Scripts/aic/core/services/*.js"));

            //  v2.0 / Bower versions
            //  ------------------------------------------------------------
            bundles.Add(new StyleBundle("~/bower/bootstrap/css").Include(
                      "~/Scripts/bower_components/bootstrap/dist/css/bootstrap.css",
                      "~/Scripts/bower_components/ngAnimate/css/ng-animation.css",
                      "~/Scripts/bower_components/angular-toastr/dist/angular-toastr.css"));

            bundles.Add(new StyleBundle("~/site/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bower/bootstrap/js").Include(
                      "~/Scripts/bower_components/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bower/jquery").Include(
                      "~/Scripts/bower_components/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bower/angular").Include(
                      "~/Scripts/bower_components/angular/angular.js",
                      "~/Scripts/bower_components/angular-animate/angular-animate.js",
                      "~/Scripts/bower_components/angular-bootstrap/ui-bootstrap.js",
                      "~/Scripts/bower_components/angular-bootstrap/ui-bootstrap-tpls.js",
                      "~/Scripts/bower_components/angular-route/angular-route.js",
                      "~/Scripts/bower_components/angular-toastr/dist/angular-toastr.js",
                      "~/Scripts/bower_components/angular-toastr/dist/angular-toastr.tpls.js"
                      ));

            //aic: this disables the optimizations in Bundles. 
            //this allows all files  to be rendered separately while we develop

            BundleTable.EnableOptimizations = false;
        }
    }
}
