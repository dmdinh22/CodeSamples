using Aic.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Aic.Web.Controllers
{
    [RoutePrefix("pagemetatags")]
    public class PageMetaTagsController : BaseController
    {
        // GET: PageMetaTags
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