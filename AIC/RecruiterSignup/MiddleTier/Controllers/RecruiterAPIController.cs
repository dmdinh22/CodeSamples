using Microsoft.AspNet.Identity.Owin;
using Aic.Web.Models.Requests.Recruiter;
using Aic.Web.Models.Responses;
using Aic.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Aic.Web.Domain;
using Microsoft.AspNet.Identity;
using Aic.Web.Models.Requests;

namespace Aic.Web.Controllers.Api
{
    [RoutePrefix("api/recruiter")]
    public class RecruiterAPIController : BaseApiController
    {
        [Route(""), HttpPost]
        public HttpResponseMessage PostRecruiter(RecruiterPostRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                SuccessResponse response = new SuccessResponse();

                RecruiterService.PostRecruiter(request);

                RecruiterService.UpgradeToRecruiter();

                ReminderPostRequest reminder = new ReminderPostRequest();
                reminder.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
                reminder.ReminderDateTime = DateTime.UtcNow;
                reminder.ReminderDateTimeString = reminder.ReminderDateTime.ToString();
                reminder.ReminderDescription = "Thank you for signing up as a Recruiter, your account is now capable of posting Jobs along with managing Jobs in your Recruiter Dashboard";
                reminder.ReminderType = 2;

                RecruiterService.PostReminder(reminder);

                UserService.Logout();

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest payload = new ErrorLogAddRequest();
                payload.ErrorFunction = "Aic.Web.Controllers.Api.PostRecruiter";
                payload.ErrorMessage = ex.Message;
                payload.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(payload);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [Route("dashboard/referral/{id:int}"), HttpGet]
        public HttpResponseMessage GetJobWithReferrals(int id)
        {
            try
            {
                ItemsResponse<JobInfo> response = new ItemsResponse<JobInfo>();

                response.Items = RecruiterService.GetJobWithReferrals(id);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetReminder";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("dashboard/reminder"), HttpGet]
        public HttpResponseMessage GetReminder()
        {
            try
            {
                ItemsResponse<Reminder> response = new ItemsResponse<Reminder>();

                response.Items = RecruiterService.GetReminder();

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetReminder";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Route("dashboard/reminder/user"), HttpGet]
        public HttpResponseMessage GetReminderByUser()
        {
            try
            {
                ItemsResponse<Reminder> response = new ItemsResponse<Reminder>();

                response.Items = RecruiterService.GetReminderByUserId();

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetReminderByUser";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Route("dashboard/reminder"), HttpPost]
        public HttpResponseMessage PostReminder(ReminderPostRequest request)
        {
            if (!ModelState.IsValid)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.PostReminder";
                error.ErrorMessage = ModelState.ToString();
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<int> response = new ItemResponse<int>();

            request.CreatedBy = HttpContext.Current.User.Identity.GetUserId();

            response.Item = RecruiterService.PostReminder(request);

            return Request.CreateResponse(response);
        }

        [Route("dashboard/reminder/{id:int}"), HttpPut]
        public HttpResponseMessage PutReminder(int id, ReminderPutRequest request)
        {
            if (ModelState.IsValid)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.PutReminder";
                error.ErrorMessage = ModelState.ToString();
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            SuccessResponse response = new SuccessResponse();

            RecruiterService.UpdateReminder(request);

            return Request.CreateResponse(response);
        }

        [Route("dashboard/reminder/{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteReminder(int id = 0)
        {
            if (id <= 0)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.DeleteReminder";
                error.ErrorMessage = id + " is not valid!";
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            SuccessResponse response = new SuccessResponse();

            RecruiterService.DeleteReminder(id);

            return Request.CreateResponse(response);
        }
    }
}