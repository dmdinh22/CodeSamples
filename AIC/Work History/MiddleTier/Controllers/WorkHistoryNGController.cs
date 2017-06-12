using Aic.Web.Controllers.Attributes;
using Aic.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aic.Web.Controllers
{
    [AnonAuthorize(Roles="User")]
    [RoutePrefix("workhistoryNG")]
    public class WorkHistoryNGController : BaseController
    {
        // GET: WorkHistoryNG
        public ActionResult Index()
        {
           return View();
        }

        [Route("create")]
        [Route("{workhistoryNGId:int?}/edit")]
        public ActionResult Manage(int? workhistoryNGId = null)
        {
            ItemViewModel<int?> vm = new ItemViewModel<int?>();
            vm.Item = workhistoryNGId;

            return View("Manage", vm);
        }
    }
}