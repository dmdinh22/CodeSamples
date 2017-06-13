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
    [RoutePrefix("api/blogs")]
    public class BlogsApiController : BaseApiController
    {
        [Route("")] // <== route to your method
        [HttpPost] // <== the type of Http Methods Supported (Post, Put, Get, Delete) - route already targets api/blogs, why we don't need to put a route
        public HttpResponseMessage CreateBlog([FromBody] BlogAddRequest payload)
        {
            // verify that payload has values in every property
            if (string.IsNullOrWhiteSpace(payload.Title) ||
                string.IsNullOrWhiteSpace(payload.Content) ||
                payload.AuthorId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            // Create our response model
            ItemResponse<int> response = new ItemResponse<int>();

            //string title, string text, string createdUser
            response.Item = BlogService.BlogInsert(payload.Title, payload.Content, payload.AuthorId);

            return Request.CreateResponse(response);
        } //CreateBlog

        [Route("")] // <== route to your method
        [HttpGet] // <== the type of Http Methods Supported (Post, Put, Get, Delete)
        public HttpResponseMessage GetBlogs()
        {
            ItemsResponse<Blog> response = new ItemsResponse<Blog>(); // pass the response from class through ItemsResponse

            response.Items = BlogService.BlogSelect(); // method inherited from service that inherits from a class

            return Request.CreateResponse(response);
        } // GetBlogs

        [Route("{blogId}")] // <== route to your method
        [HttpGet] // <== the type of Http Methods Supported (Post, Put, Get, Delete)
        public HttpResponseMessage GetBlogById([FromUri] int blogId)
        {
            if (blogId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ItemResponse<Blog> response = new ItemResponse<Blog>(); // pass the list Blog into response

            response.Item = BlogService.BlogSelectById(blogId);

            return Request.CreateResponse(response);
        } // GetBlogById

        [Route("{blogId}/comments")]
        [HttpGet]
        public HttpResponseMessage GetCommentsByBlogId([FromUri] int blogId)
        {

            ItemsResponse<CommentAdvanced> response = new ItemsResponse<CommentAdvanced>();

            response.Items = BlogService.BlogCommentSelect(blogId);

            return Request.CreateResponse(response);
        } // GetCommentsByBlogId

        [Route("{blogId}/advanced")]
        [HttpGet]
        public HttpResponseMessage GetCommentsAdvancedByBlogId([FromUri] int blogId)
        {
            if (blogId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ItemsResponse<Blog.Comment.Reply> response = new ItemsResponse<Blog.Comment.Reply>(); // pass the list Blog Comments into response

            response.Items = CommentAdvancedService.CommentAdvancedSelectByBlogId(blogId);

            return Request.CreateResponse(response);
        } // GetCommentsAdvancedByBlogId

        [Route("{blogId}")]
        [HttpPut]
        public HttpResponseMessage UpdateBlog([FromUri] int blogId, [FromBody] BlogUpdateRequest payload) // pass in both FromUri & FromBody
        {
            if (string.IsNullOrWhiteSpace(payload.Title) ||
              string.IsNullOrWhiteSpace(payload.Content) ||
              payload.AuthorId <= 0 ||
              blogId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            SuccessResponse response = new SuccessResponse(); // setting Class SuccessResponse to response

            // In here, we need to update the blog by ID
            BlogService.BlogUpdate(blogId, payload.Title, payload.Content, payload.AuthorId);

            return Request.CreateResponse(response); // passing in response to the Create Response

        } // UpdateBlogs

        [Route("{blogId}")]
        [HttpDelete]
        public HttpResponseMessage DeleteBlog([FromUri] int blogId)
        {

            if (blogId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            SuccessResponse response = new SuccessResponse();

            // In here, we need to delete the blog by ID
            BlogService.BlogDelete(blogId);

            return Request.CreateResponse(response);
        } // DeleteBlog

        [Route("{blogId}/author")]
        [HttpGet]
        public HttpResponseMessage GetBlogAuthor([FromUri] int id) // id = blogId
        {
            if (id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ItemResponse<Author> response = new ItemResponse<Author>();

            response.Item = AuthorService.AuthorSelectByBlogId(id);

            return Request.CreateResponse(response);

        } // GetBlogAuthorById

        [Route("helloworld/{name}"), HttpGet] // <== api/blogs/helloworld/{name} example
        public HttpResponseMessage HelloWorld([FromUri] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse("No name was passed..."));
            }

            ItemResponse<string> response = new ItemResponse<string>();
            response.Item = name;

            return Request.CreateResponse(response);
        } // <== example

    }
}
