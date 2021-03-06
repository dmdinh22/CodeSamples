﻿using Microsoft.AspNet.Identity;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/referral")]
    public class ReferralApiController : ApiController
    {
        IReferralService _referralService = null;
        IMessagingService _messagingService = null;


        public ReferralApiController(IReferralService referralService, IMessagingService messagingService)
        {
            _referralService = referralService;
            _messagingService = messagingService;

        }

        //POST Referral pending

        [Route(""), HttpPut]
        public HttpResponseMessage CreateUpdateReferral([FromBody] ReferralRequest payload)
        {
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();

                response.Item = _referralService.ReferralUpdateInsert(payload);

                if (payload.Accepted == true) {
                    ConversationMsgAddRequest convo = new ConversationMsgAddRequest();
                    convo.SenderId = HttpContext.Current.User.Identity.GetUserId();
                    convo.ReceiverId = payload.CandidateGuid;
                    convo.Subject = "Referral Request Accepted!";
                    var request = HttpContext.Current.Request;
                    var address = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);
                    convo.Content = "Check your 'My Referrals' page to see your updated referral requests here: " + address + "/Jobs/MyReferrals/";
                    _messagingService.CreateConversationMsg(convo);
                }

                return Request.CreateResponse(response);
            }
            catch (System.Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.CreateReferral";
                error.ErrorMessage = ModelState.ToString();
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //GET Referral Pending
        [Route("{referrerId:int}/{candidateId:int}/{jobId:int}"), HttpGet]
        public HttpResponseMessage GetPendingReferralByIds([FromUri] int referrerId, int candidateId, int jobId)
        {

                ItemsResponse<ReferralPending> response = new ItemsResponse<ReferralPending>();

                response.Items = _referralService.GetPendingReferral(referrerId, candidateId, jobId);

                return Request.CreateResponse(response);
            
        }

        [Route("{ID:int}"), HttpGet]
        public HttpResponseMessage GetMyReferrals([FromUri] int ID)
        {

            ItemsResponse<ReferralsJob> response = new ItemsResponse<ReferralsJob>();

            response.Items = _referralService.GetMyReferrals(ID);

            return Request.CreateResponse(response);

        }
    }
}
