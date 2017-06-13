using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dinh.Mvc.Models.Requests.Blogs
{
    public class AuthorUpdateRequest
    {
        [Required, Range(1, 2147483647)]
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
    }
}