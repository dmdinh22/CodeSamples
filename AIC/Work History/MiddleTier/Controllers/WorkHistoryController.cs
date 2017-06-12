using Aic.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aic.Web.Controllers
{
    //[RoutePrefix("workhistory")]
    public class WorkHistoryController : BaseController
    {
        // GET: WorkHistory
        public ActionResult Index()
        {
           HomePageViewModel model = new HomePageViewModel();
           return View(model);
        }

        [Route("{id:int?}/edit")]
        [Route("create")]
        public ActionResult CreateEdit(int? id = null)
        {
           ItemViewModel<int?> vm = new ItemViewModel<int?>();
           vm.Item = id;

           return View("CreateEdit", vm);
        }
    }
}