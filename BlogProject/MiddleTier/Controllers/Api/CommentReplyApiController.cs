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
    [RoutePrefix("api/commentreplies")]
    public class CommentReplyApiController : BaseApiController
    {
        [Route("")]
        [HttpPost]
        public HttpResponseMessage CreateCommentReply([FromBody] CommentReplyAddRequest payload)
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

            response.Item = CommentReplyService.CommentReplyInsert(payload.ParentCommentId, payload.Author, payload.Title, payload.Content);

            return Request.CreateResponse(response);
        } //CreateCommentReply

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetCommentReplies()
        {
            ItemsResponse<CommentAdvanced> response = new ItemsResponse<CommentAdvanced>();

            response.Items = CommentReplyService.CommentReplySelect();

            return Request.CreateResponse(response);
        } //GetCommentReplies

        [Route("{commentReplyId}")]
        [HttpGet]
        public HttpResponseMessage GetCommentReplyById([FromUri] int commentReplyId)
        {
            if (commentReplyId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ItemResponse<CommentAdvanced> response = new ItemResponse<CommentAdvanced>();

            response.Item = CommentReplyService.CommentReplySelectById(commentReplyId);

            return Request.CreateResponse(response);
        } //GetCommentReplyById

        [Route("{id}")] // <== route of method
        [HttpPut] // <== type of http methods supported
        public HttpResponseMessage UpdateCommentReply([FromUri] int id, [FromBody] CommentReplyUpdateRequest payload)
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

            CommentReplyService.CommentReplyUpdate(payload.id, payload.Title, payload.Content);

            return Request.CreateResponse(response);
        } // UpdateCommentReply

        [Route("{commentReplyId}")] // <== Route of Method
        [HttpDelete] // <== type of http methods supported
        public HttpResponseMessage DeleteCommentReply([FromUri] int commentReplyId)
        {
            if (commentReplyId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // create our response model
            SuccessResponse response = new SuccessResponse();

            CommentReplyService.CommentReplyDelete(commentReplyId);

            return Request.CreateResponse(response);
        } // DeleteCommentReply

    }
}