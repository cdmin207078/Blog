using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Blog
{
    public class BundleConfig
    {
        public static void RegsiterBundles(BundleCollection Bundles)
        {
            Bundles.Add(new ScriptBundle("~/bundles/JS").Include(
                "~/Content/Scripts/jquery-{version}.js"));

            Bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                "~/Content/Css/bootstrap-{version}.css",
                "~/Content/Css/diy-plugin.css",
                "~/Content/Css/common.css"));


            Bundles.Add(new ScriptBundle("~/bundles/simditor/JS").Include(
                "~/Content/simditor/scripts/module.js",
                "~/Content/simditor/scripts/hotkeys.js",
                "~/Content/simditor/scripts/uploader.js",
                "~/Content/simditor/scripts/simditor.js"));

            Bundles.Add(new StyleBundle("~/bundles/simditor/Css").Include(
                "~/Content/simditor/styles/simditor.css"));
        }
    }
}