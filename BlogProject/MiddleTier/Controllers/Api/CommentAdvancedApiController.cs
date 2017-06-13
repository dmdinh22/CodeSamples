using Dinh.Mvc.Domain.Blogs;
using Dinh.Mvc.Models.Requests.Blogs;
using Dinh.Mvc.Models.Responses;
using Dinh.Mvc.Services;
using Dinh.Mvc.Services.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Dinh.Mvc.Controllers.Api
{
    [RoutePrefix("api/commentadvanced")]
    public class CommentAdvancedApiController : BaseApiController
    {
        [Route("")]
        [HttpPost]
        public HttpResponseMessage CreateCommentAdvanced([FromBody] CommentAdvancedAddRequest payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Title) ||
               string.IsNullOrWhiteSpace(payload.Content) ||
               string.IsNullOrWhiteSpace(payload.Author) ||
               payload.ParentCommentId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // create our response model
            ItemResponse<int> response = new ItemResponse<int>();

            response.Item = CommentAdvancedService.CommentAdvancedInsert(payload.BlogPostId, payload.ParentCommentId, payload.Author, payload.Title, payload.Content);

            return Request.CreateResponse(response);

        } //CreateCommentAdvanced

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetCommentAdvanced()
        {
            ItemsResponse<Blog.Comment.Reply> response = new ItemsResponse<Blog.Comment.Reply>();

            response.Items = CommentAdvancedService.CommentAdvancedSelect();

            return Request.CreateResponse(response);
        } //GetCommentAdvanced

        [Route("{commentAdvancedId}")]
        [HttpGet]
        public HttpResponseMessage GetCommentAdvancedById([FromUri] int commentAdvancedId)
        {
            if (commentAdvancedId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ItemResponse<Blog.Comment.Reply> response = new ItemResponse<Blog.Comment.Reply>();

            response.Item = CommentAdvancedService.CommentAdvancedSelectById(commentAdvancedId);

            return Request.CreateResponse(response);
        } //GetCommentAdvancedById

        [Route("{id}")] // <== route of method
        [HttpPut] // <== type of http methods supported
        public HttpResponseMessage UpdateCommentAdvanced([FromUri] int id, [FromBody] CommentAdvancedUpdateRequest payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Title) ||
                string.IsNullOrWhiteSpace(payload.Content) ||
                string.IsNullOrWhiteSpace(payload.Author) ||
                payload.ParentCommentId <= 0 ||
                id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // create our response model
            SuccessResponse response = new SuccessResponse();

            CommentAdvancedService.CommentAdvancedUpdate(payload.id, payload.Title, payload.Content);

            return Request.CreateResponse(response);
        } // UpdateCommentAdvanced

        [Route("{commentAdvancedId}")] // <== Route of Method
        [HttpDelete] // <== type of http methods supported
        public HttpResponseMessage DeleteCommentAdvanced([FromUri] int commentAdvancedId)
        {
            if (commentAdvancedId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // create our response model
            SuccessResponse response = new SuccessResponse();

            CommentAdvancedService.CommentAdvancedDelete(commentAdvancedId);

            return Request.CreateResponse(response);
        } // DeleteCommentAdvanced


    }
}