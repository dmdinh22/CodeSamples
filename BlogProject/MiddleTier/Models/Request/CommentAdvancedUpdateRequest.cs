using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dinh.Mvc.Models.Requests.Blogs
{
    public class CommentAdvancedUpdateRequest
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int BlogPostId { get; set; }

        public int ParentCommentId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}