using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dinh.Mvc.Domain
{
    public class CommentAdvanced
    {
        public int ID { get; set; }
        public int BlogPostID { get; set; }
        public int? ParentCommentID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public List<CommentAdvanced> Replies { get; set; } = null;
    }
}