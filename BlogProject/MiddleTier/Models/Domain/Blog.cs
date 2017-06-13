using System;

namespace Dinh.Mvc.Domain.Blogs
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public class Comment
        {
            public int ID { get; set; }
            public int BlogPostID { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string UserName { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateModified { get; set; }


            public class Reply : Comment
            {
                public int ParentCommentID { get; set; }
            }
        }
    }
}