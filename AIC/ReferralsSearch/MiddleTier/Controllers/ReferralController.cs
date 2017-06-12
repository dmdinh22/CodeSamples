using aic.Web.Controllers.Attributes;
using aic.Web.Models.Requests;
using aic.Web.Models.ViewModels;
using System.Web.Mvc;

namespace aic.Web.Controllers
{
    [AnonAuthorize(Roles="User")]
    [RoutePrefix("referrals")]
    public class ReferralController : BaseController
    {
        [Route("sender/{candidateId:int}/job/{jobId:int}")]
        public ActionResult Message(int candidateId, int jobId)
        {
            ItemViewModel<ReferralRequest> model = new ItemViewModel<ReferralRequest>();

            model.Item = new ReferralRequest
            {
                CandidateId = candidateId,
                JobId = jobId
            };

            return View(model);
        }
    }
}