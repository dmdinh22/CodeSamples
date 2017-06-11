using Aic.Web.Domain;
using Aic.Web.Models;
using Aic.Web.Models.Requests;
using Aic.Web.Models.Responses;
using Aic.Web.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aic.Web.Controllers.Api
{
    [RoutePrefix("api/metatags")]
    public class MetaTagsApiController : ApiController
    {
        IMetaTagsService _metaTagsService = null;

        public MetaTagsApiController(IMetaTagsService metaTagsService)
        {
            _metaTagsService = metaTagsService;
        }

        #region MetaTag API Endpoints

        [Route("page/{id:int}")]
        [HttpPost]
        public HttpResponseMessage CreatePageMetaTag([FromUri] int id, [FromBody] Dictionary<string, string> payload)
        {

            try
            {
                List<PageMetaTags> pageMetaTagList = new List<PageMetaTags>();

                foreach (var kv_pair in payload)
                {
                    MtEnum metaTag;
                    bool success = Enum.TryParse<MtEnum>(kv_pair.Key, out metaTag);
                    if (success)
                    {
                        PageMetaTags pmt = new PageMetaTags();
                        pmt.MetaTagValue = kv_pair.Value;
                        pmt.MetaTagID = metaTag;
                        pageMetaTagList.Add(pmt);
                    }
                }

                // creating a new object to match requested parameter for the service
                PageMetaTagsAddRequest request = new PageMetaTagsAddRequest(); // instantiating model
                request.OwnerTypeId = id;
                request.MetaTags = pageMetaTagList; // set MetaTags property of model to a variable 

                _metaTagsService.PageMetaTagsInsert(request); // pass model into web service

                // confirming that everything went OK
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.CreatePageMetaTag";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } // CreatePageMetaTag

        [Route("page")]
        [HttpGet]
        public HttpResponseMessage GetAllPageMetaTags()
        {
            try
            {
                ItemsResponse<PageMetaTags> response = new ItemsResponse<PageMetaTags>();

                response.Items = _metaTagsService.PageMetaTagsSelectAll();

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetAllPageMetaTags";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } //GetAllPageMetaTags

        [Route("page/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetPageMetaTagsById([FromUri] int id)
        {
            try
            {
                ItemResponse<PageMetaTags> response = new ItemResponse<PageMetaTags>();

                response.Item = _metaTagsService.PageMetaTagsSelectById(id);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetPageMetaTagsById";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } //GetPageMetaTagsById

        [Route("page/{id:int}")]
        [HttpPut]
        public HttpResponseMessage UpdatePageMetaTags([FromUri] int id, [FromBody] PageMetaTagsUpdateRequest payload)
        {
            try
            {
                SuccessResponse response = new SuccessResponse();

                _metaTagsService.PageMetaTagsUpdate(payload);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.UpdatePageMetaTags";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } //UpdatePageMetaTags

        [Route("page/{id:int}")]
        [HttpDelete]
        public HttpResponseMessage DeletePageMetaTags([FromUri] int id)
        {

            if (id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            SuccessResponse response = new SuccessResponse();

            _metaTagsService.PageMetaTagsDelete(id);

            return Request.CreateResponse(response);


        } //DeletePageMetaTags

        [Route]
        [HttpGet]
        public HttpResponseMessage GetAllMetaTags()
        {
            try
            {
                ItemsResponse<MetaTags> response = new ItemsResponse<MetaTags>();

                response.Items = _metaTagsService.MetaTagsSelectAll();

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetAllMetaTags";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } //GetAllMetaTags

        [Route("{*ownerName}")]
        [HttpGet]
        public HttpResponseMessage GetMetaTagsByOwnerName([FromUri] string ownerName)
        {
            try
            {
                ItemsResponse<PageMetaTags> response = new ItemsResponse<PageMetaTags>();

                response.Items = _metaTagsService.PageMetaTags_SelectAllByOwnerName(ownerName);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Aic.Web.Controllers.Api.GetMetaTagsByOwnerName";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        } //GetMetaTagsByOwnerName

        #endregion

        #region OwnerType API Endpoints
        [Route("ownertype")]
        [HttpGet]
        public HttpResponseMessage GetAllOwnerTypes()
        {
            ItemsResponse<OwnerType> response = new ItemsResponse<OwnerType>();

            response.Items = _metaTagsService.OwnerType_SelectAll();

            return Request.CreateResponse(response);
        }//GetAllOwnerTypes

        [Route("ownertype")]
        [HttpPost]
        public HttpResponseMessage CreateOwnerType([FromBody] OwnerTypeAddRequest payload)
        {
            ItemResponse<int> response = new ItemResponse<int>();

            response.Item = _metaTagsService.OwnerTypeInsert(payload);

            return Request.CreateResponse(response);
        } // CreateOwnerType

        [Route("ownertype/{id:int}")]
        [HttpDelete]
        public HttpResponseMessage DeleteOwnerType([FromUri] int id)
        {
            if (id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            SuccessResponse response = new SuccessResponse();

            _metaTagsService.OwnerTypeDelete(id);

            return Request.CreateResponse(response);
        } //DeletePageMetaTags
        #endregion

        #region Single Page MetTag API Endpoints
        [Route("single/{*name}")]
        [HttpGet]
        public HttpResponseMessage GetSinglePageMetaTagsByOwnerType([FromUri] string name)
        {
            ItemsResponse<SinglePageMetaTags> response = new ItemsResponse<SinglePageMetaTags>();

            response.Items = _metaTagsService.SinglePageMetaTags_SelectAllByOwnerType(name);

            return Request.CreateResponse(response);
        }

        [Route("single/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetSinglePageMetaTagsByOwnerTypeId([FromUri] int id)
        {
            ItemsResponse<SinglePageMetaTags> response = new ItemsResponse<SinglePageMetaTags>();

            response.Items = _metaTagsService.SinglePageMetaTags_SelectAllByOwnerTypeId(id);

            return Request.CreateResponse(response);

        } //GetPageMetaTagsByOwnerTypeId

        #endregion

    }
}
