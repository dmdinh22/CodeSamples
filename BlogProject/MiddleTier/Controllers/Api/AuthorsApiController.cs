using Dinh.Mvc.Domain;
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
    [RoutePrefix("api/authors")]
    public class AuthorsApiController : BaseApiController
    {
        [Route("generate/{email}")]
        [HttpGet]
        public HttpResponseMessage GenerateAuthor([FromUri] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse("Must enter a name."));
            }

            // By this point we know name is not null or whitespace.

            ItemResponse<int> response = new ItemResponse<int>();

            // here we are doing our addditional processing.
            response.Item = AuthorService.AuthorInsert(email);

            return Request.CreateResponse(response);

        } // GenerateAuthor

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetAuthorById([FromUri] int id)
        {
            ItemResponse<Author> response = new ItemResponse<Author>();

            // In here, we need to retrieve the author by ID
            response.Item = AuthorService.AuthorSelectById(id);


            return Request.CreateResponse(response);
        } //GetAuthorById

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAuthors()
        {
            ItemsResponse<Author> response = new ItemsResponse<Author>();

            // In here we need to retrieve all authors.
            response.Items = AuthorService.AuthorSelect();

            return Request.CreateResponse(response);
        } // GetAuthors

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteAuthor([FromUri] int id)
        {
            SuccessResponse response = new SuccessResponse();

            // In here, we need to delete the author by ID
            AuthorService.AuthorDelete(id);

            return Request.CreateResponse(response);
        } //DeleteAuthor
    }
}
