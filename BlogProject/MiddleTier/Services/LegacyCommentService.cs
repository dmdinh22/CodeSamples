using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dinh.Mvc.Domain.Blogs;
using Dinh.Data;

namespace Dinh.Mvc.Services
{
    /// <summary>
    /// This class will handle all of the connections between the database and our 
    /// project as it pertains to the Comments.
    /// </summary>
    public class LegacyCommentService : BaseService
    {
        
        public static int CommentInsert(int blogPostId, string title, string content, string userName)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Comment_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@BlogPostId", blogPostId);
                   paramCollection.AddWithValue("@Title", title);
                   paramCollection.AddWithValue("@Content", content);
                   paramCollection.AddWithValue("@UserName", userName);

                   SqlParameter p = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection paramCollection)
               {
                   int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
               }
               );
            return id;
        } // CommentInsert

        public static List<Blog.Comment> CommentSelect()
        {
            List<Blog.Comment> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Comment_Select"
               , inputParamMapper: null, map: delegate (IDataReader reader, short set)
               {
                   Blog.Comment b = new Blog.Comment();
                   int startingIndex = 0;

                   b.ID = reader.GetSafeInt32(startingIndex++);
                   b.BlogPostID = reader.GetSafeInt32(startingIndex++);
                   b.Title = reader.GetSafeString(startingIndex++);
                   b.Content = reader.GetSafeString(startingIndex++);
                   b.UserName = reader.GetSafeString(startingIndex++);
                   b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                   b.DateModified = reader.GetSafeDateTime(startingIndex++);

                   if (list == null)
                   {
                       list = new List<Blog.Comment>();
                   }

                   list.Add(b);
               }
               );


            return list;
        } //CommentSelect

        public static List<Blog.Comment> CommentSelectByBlogId(int blogId)
        {
            List<Blog.Comment> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Comment_SelectByBlogId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@BlogPostId", blogId);
              }, map: delegate (IDataReader reader, short set)
              {
                  Blog.Comment b = new Blog.Comment();
                  int startingIndex = 0;

                  b.BlogPostID = reader.GetSafeInt32(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.UserName = reader.GetSafeString(startingIndex++);
                  b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                  b.DateModified = reader.GetSafeDateTime(startingIndex++);

                  if (list == null)
                  {
                      list = new List<Blog.Comment>();
                  }

                  list.Add(b);
              }
              );

            return list;
        } // CommentSelectByBlogId -- in blogsapicontroller

        public static Blog.Comment CommentSelectById(int commentId)
        {
            Blog.Comment row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Comment_SelectById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", commentId);
              }, map: delegate (IDataReader reader, short set)
              {
                  Blog.Comment b = new Blog.Comment();
                  int startingIndex = 0;

                  b.ID = reader.GetSafeInt32(startingIndex++);
                  b.BlogPostID = reader.GetSafeInt32(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.UserName = reader.GetSafeString(startingIndex++);
                  b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                  b.DateModified = reader.GetSafeDateTime(startingIndex++);

                  if (row == null)
                  {
                      row = b;
                  }
              }
              );

            return row;
        } // CommentSelectById

        public static void CommentUpdate(int commentId, string title, string content)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Comment_Update"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", commentId);
                  paramCollection.AddWithValue("@Title", title);
                  paramCollection.AddWithValue("@Content", content);

              }, returnParameters: null
              );
        } // CommentUpdate

        public static void CommentDelete(int commentId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Comment_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ID", commentId);
               }, returnParameters: null
               );

        } // CommentDelete
        
    }

}