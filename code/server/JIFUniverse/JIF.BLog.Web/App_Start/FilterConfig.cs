using System.Web;
using System.Web.Mvc;

namespace JIF.Blog.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new JIFExceptionAttribute());
            //filters.Add(new HandleErrorAttribute());

        }
    }
}