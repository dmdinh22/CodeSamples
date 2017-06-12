using Aic.Web.Controllers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Aic.Web.Controllers
{
    public class RecruiterController : BaseController
    {
        [RecCreateAuthorize(Roles="User")]
        public ActionResult Create()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [RecruiterAuthorize(Roles = "Recruiter")]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}