using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Models.ViewModels;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/profile")]
    public class ProfileAPIController : ApiController
    {
        IProfileService _profileService = null;
        public ProfileAPIController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        // Selects All Profiles
        [Route(""), HttpGet]
        public HttpResponseMessage ProfileSelect()
        {
            try
            {
                ItemsResponse<Profile> response = new ItemsResponse<Profile>();

                response.Items = _profileService.GetProfile();

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.ProfileSelect";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        // Selects Profile By ID
        [Route("{id}"), HttpGet]
        public HttpResponseMessage ProfileSelectByID(int id)
        {
            try
            {
                ItemResponse<Profile> response = new ItemResponse<Profile>();

                response.Item = _profileService.GetProfileByID(id);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.ProfileSelectById";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        // Inserts a Profile
        [Route(""), HttpPost]
        public HttpResponseMessage ProfileInsert([FromBody] ProfileAddRequest payload)
        {
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();

                response.Item = _profileService.InsertProfile(payload);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.ProfileInsert";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        // Updates a Profile
        [Route(""), HttpPut]
        public HttpResponseMessage ProfileUpdate([FromBody] ProfileUpdateRequest payload)
        {
            try
            {
                SuccessResponse response = new SuccessResponse();

                _profileService.UpdateProfile(payload);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.ProfileUpdate";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        // Delete a Profile
        [Route("{id}"), HttpDelete]
        public HttpResponseMessage ProfileDelete([FromUri] int id)
        {
            try
            {
                SuccessResponse response = new SuccessResponse();

                _profileService.DeleteProfile(id);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.ProfileDelete";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        // Selects a ProfilePage
        [Route("Home/{id}"), HttpGet]
        public HttpResponseMessage ProfilePageSelect([FromUri] int id)
        {
            try
            {
                ProfileViewModel vm = new ProfileViewModel();

                vm = ProfileService.GetProfilePage(id);

                return Request.CreateResponse(vm);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.ProfilePageSelect";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        // Select Profiles by Company Match
        [Route("ReferralSearch/{personId:int}/{pageNum:int}/{pageSize:int}")]
        [HttpGet]
        public HttpResponseMessage GetReferralsPaginated([FromUri] int personId, int pageNum, int pageSize)
        {
            try
            {
                ItemsResponse<ReferralProfileSearch> response = new ItemsResponse<ReferralProfileSearch>();

                response.Items = _profileService.GetReferralsPaginated(personId, pageNum, pageSize);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.GetReferralsPaginated";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
