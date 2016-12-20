using System.IO;
using System.Web.Mvc;

namespace JIF.Blog.Web.Areas.Admin.Controllers
{
    public class FileUploadController : Controller
    {

        // GET: Admin/FileUpload
        [HttpPost]
        public JsonResult Index()
        {
            var files = Request.Files;

            if (files != null && files.Count > 0)
            {
                string savepath = Path.Combine(Server.MapPath("~/upload/images/"), files[0].FileName);
                files[0].SaveAs(savepath);

                return Json(new { link = files[0].FileName });
            }
            else
            {
                return Json(new { success = false, message = "没有文件" });
            }
        }
    }
}