using Aic.Web.Controllers.Attributes;
using Aic.Web.Models.ViewModels;
using System.Web.Mvc;

namespace Aic.Web.Controllers
{
    [RoutePrefix("ProfileNG")]
    public class ProfileNGController : BaseController
    {
        GET: ProfileNG
        [Route("Index")]
        public ActionResult Index()
        {
           return View();
        }

        [Route("Create")]
        [Route("{id:int}/Edit")]
        public ActionResult Manage(int? id = 0)
        {
           ItemViewModel<int?> model = new ItemViewModel<int?>();

           model.Item = id;

           return View(model);
        }

        [Route("landing")]
        public ActionResult Landing()
        {
           return View("landing");
        }
        [AnonAuthorize(Roles ="User")]
        [Route("search/{jobId:int?}")]
        public ActionResult ReferralSearch(int jobId)
        {
            ItemViewModel<int?> model = new ItemViewModel<int?>();
            model.Item = jobId;
            return View(model);
        }
        [AnonAuthorize(Roles = "User")]
        [Route("search/email/{jobId:int}")]
        public ActionResult EmailRef(int jobId)
        {
            ItemViewModel<int?> model = new ItemViewModel<int?>();

            model.Item = jobId;

            return View(model);
        }
    }
}