using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dinh.Mvc.Models.Requests.Blogs
{
    public class BlogAddRequest
    {
        [Required, Range(1, 2147483647)]
        public int AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

    }
}