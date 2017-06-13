using Dinh.Mvc.Domain;
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
    [RoutePrefix("api/comments")]
    public class CommentsApiController : BaseApiController
    {
        [Route("")] // <== route of method
        [HttpPost] // <== type of http methods supported
        public HttpResponseMessage CreateComment([FromBody] CommentAddRequest payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Title) ||
                string.IsNullOrWhiteSpace(payload.Content) ||
                string.IsNullOrWhiteSpace(payload.UserName) ||
                payload.BlogPostId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // create our response model
            ItemResponse<int> response = new ItemResponse<int>();

            response.Item = CommentService.CommentInsert(payload.BlogPostId, payload.ParentCommentId, payload.UserName, payload.Title, payload.Content);

            return Request.CreateResponse(response);
        } //CreateComment

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetComments()
        {
            ItemsResponse<CommentAdvanced> response = new ItemsResponse<CommentAdvanced>();

            response.Items = CommentService.CommentSelect();

            return Request.CreateResponse(response);
        } //GetComments

        [Route("{commentId}")]
        [HttpGet]
        public HttpResponseMessage GetCommentById([FromUri] int commentId)
        {
            if (commentId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ItemResponse<CommentAdvanced> response = new ItemResponse<CommentAdvanced>();

            response.Item = CommentService.CommentSelectById(commentId);

            return Request.CreateResponse(response);
        } //GetCommentById

        [Route("{commentId}/replies")]
        [HttpGet]
        public HttpResponseMessage GetCommentRepliesByCommentId([FromUri] int commentId)
        {
            if (commentId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ItemsResponse<CommentAdvanced> response = new ItemsResponse<CommentAdvanced>(); // pass the list Replies into response

            response.Items = CommentReplyService.CommentReplySelectByCommentId(commentId);

            return Request.CreateResponse(response);
        } //GetCommentRepliesByCommentId

        [Route("{id}")] // <== route of method
        [HttpPut] // <== type of http methods supported
        public HttpResponseMessage UpdateComment([FromUri] int id, [FromBody] CommentUpdateRequest payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Title)    ||
                string.IsNullOrWhiteSpace(payload.Content)  ||
                string.IsNullOrWhiteSpace(payload.UserName) ||
                payload.BlogPostId <= 0 ||
                id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // create our response model
            SuccessResponse response = new SuccessResponse();

            CommentService.CommentUpdate(payload.id, payload.Title, payload.Content);

            return Request.CreateResponse(response);
        } // UpdateComment

        [Route("{commentId}")] // <== Route of Method
        [HttpDelete] // <== type of http methods supported
        public HttpResponseMessage DeleteComment([FromUri] int commentId)
        {
            if (commentId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // create our response model
            SuccessResponse response = new SuccessResponse();

            CommentService.CommentDelete(commentId);

            return Request.CreateResponse(response);
        } // DeleteComment


    }
}