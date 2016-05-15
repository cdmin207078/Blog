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
            Bundles.Add(new ScriptBundle("~/bundles/JS").Include("~/Content/Scripts/jquery-{version}.js"));

            Bundles.Add(new StyleBundle("~/bundles/bootstrap").Include("~/Content/Css/bootstrap-{version}.css", "~/Content/Css/diy-bootstrap.css", "~/Content/Css/common.css"));
        }
    }
}