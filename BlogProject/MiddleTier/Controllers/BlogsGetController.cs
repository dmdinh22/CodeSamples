using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dinh.Mvc.Controllers
{
    public class BlogsGetController : BaseController
    {
        // GET: BlogsGet
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}