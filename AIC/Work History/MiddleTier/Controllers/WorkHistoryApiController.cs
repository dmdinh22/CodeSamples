using Aic.Web.Domain;
using Aic.Web.Models.Requests;
using Aic.Web.Models.Responses;
using Aic.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aic.Web.Controllers.Api
{
    [RoutePrefix("api/workhistory")]
    public class WorkHistoryApiController : ApiController
    {
        IWorkHistoryService _workHistoryService = null;

        public WorkHistoryApiController(IWorkHistoryService workHistoryService)
        {
            _workHistoryService = workHistoryService;
        } //WorkHistoryApiController

        [Route("")]
        [HttpPost]
        public HttpResponseMessage CreateWorkHistory([FromBody] WorkHistoryAddRequest payload)
        {
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();

                response.Item = _workHistoryService.WorkHistoryInsert(payload);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.CreateWorkHistory";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } // CreateWorkHistory

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAllWorkHistory()
        {
            try
            {
                ItemsResponse<WorkHistory> response = new ItemsResponse<WorkHistory>();

                response.Items = _workHistoryService.WorkHistorySelectAll();

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetAllWorkHistory";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } //GetAllWorkHistory


        [Route("{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetWorkHistoryById([FromUri] int id)
        {
            try
            {
                ItemResponse<WorkHistory> response = new ItemResponse<WorkHistory>();

                response.Item = _workHistoryService.WorkHistorySelectById(id);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetWorkHistoryById";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        } //GetWorkHistoryById

        [Route("{id:int}")]
        [HttpPut]
        public HttpResponseMessage UpdateWorkHistory([FromUri] int id, [FromBody] WorkHistoryUpdateRequest payload)
        {
            try
            {
                SuccessResponse response = new SuccessResponse();

                _workHistoryService.WorkHistoryUpdate(payload);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.UpdateWorkHistory";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } //UpdateWorkHistory

        [Route("{id:int}")]
        [HttpDelete]
        public HttpResponseMessage DeleteWorkHistory([FromUri] int id)
        {
            if (id <= 0)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.Login";
                error.ErrorMessage = id + " is not a valid ID!";
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            SuccessResponse response = new SuccessResponse();

            _workHistoryService.WorkHistoryDelete(id);

            return Request.CreateResponse(response);
        } //DeleteWorkHistory

        [Route("PersonId/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetWorkHistoryByPersonId([FromUri] int id)
        {
            try
            {
                ItemsResponse<WorkHistory> response = new ItemsResponse<WorkHistory>();

                response.Items = _workHistoryService.WorkHistorySelectByPersonId(id);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetWorkHistoryByPersonId";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } //GetWorkHistoryByPersonId




        [Route("user/{pid:int}"), HttpGet]
        public HttpResponseMessage WorkHistorySelectByUserId([FromUri] int pid)
        {
            try
            {
                ItemsResponse<WorkHistory> response = new ItemsResponse<WorkHistory>();
                response.Items = _workHistoryService.WorkHistorySelectByUserId(pid);
                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.WorkHistorySelectByUserId";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
