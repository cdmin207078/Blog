using JIF.Web.Framework.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Web.Framework.Controllers
{
    [AdminAuthorize]
    public abstract class AdminControllerBase : BaseController
    {
    }
}
