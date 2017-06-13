using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dinh.Data;
using Dinh.Mvc.Domain.Blogs;

namespace Dinh.Mvc.Services
{
    /// <summary>
    /// This class will handle all of the connections between the database and our
    /// project as it pertains to the Comment Advanced
    /// </summary>
    public class CommentAdvancedService : BaseService
    {
        public static int CommentAdvancedInsert(int blogPostId, int? parentCommentId, string author, string title, string content)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@BlogPostId", blogPostId);
                   paramCollection.AddWithValue("@Author", author);
                   paramCollection.AddWithValue("@Title", title);
                   paramCollection.AddWithValue("@Content", content);


                   SqlParameter p = paramCollection.Add("@ParentCommentId", SqlDbType.Int);
                   if (parentCommentId == null)
                       p.Value = System.DBNull.Value;
                   else
                       p.Value = parentCommentId;

                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection paramCollection)
               {
                   int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
               }
               );
            return id;
        } //CommentAdvancedInsert

        public static List<Blog.Comment.Reply> CommentAdvancedSelect()
        {
            List<Blog.Comment.Reply> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_Select"
               , inputParamMapper: null, map: delegate (IDataReader reader, short set)
               {
                   Blog.Comment.Reply b = new Blog.Comment.Reply();
                   int startingIndex = 0;

                   b.ID = reader.GetSafeInt32(startingIndex++);
                   b.BlogPostID = reader.GetSafeInt32(startingIndex++);
                   b.ParentCommentID = reader.GetSafeInt32(startingIndex++);
                   b.UserName = reader.GetSafeString(startingIndex++);
                   b.Title = reader.GetSafeString(startingIndex++);
                   b.Content = reader.GetSafeString(startingIndex++);
                   b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                   b.DateModified = reader.GetSafeDateTime(startingIndex++);

                   if (list == null)
                   {
                       list = new List<Blog.Comment.Reply>();
                   }

                   list.Add(b);
               }
               );


            return list;
        } //CommentAdvancedSelect

        public static List<Blog.Comment.Reply> CommentAdvancedSelectByBlogId(int blogId)
        {
            List<Blog.Comment.Reply> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectByBlogId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@BlogPostId", blogId);
              }, map: delegate (IDataReader reader, short set)
              {
                  Blog.Comment.Reply b = new Blog.Comment.Reply();
                  int startingIndex = 0;

                  b.BlogPostID = reader.GetSafeInt32(startingIndex++);
                  b.ParentCommentID = reader.GetSafeInt32(startingIndex++);
                  b.UserName = reader.GetSafeString(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                  b.DateModified = reader.GetSafeDateTime(startingIndex++);

                  if (list == null)
                  {
                      list = new List<Blog.Comment.Reply>();
                  }

                  list.Add(b);
              }
              );

            return list;
        } // CommentAdvancedSelectByBlogId -- in blogsapicontroller

        public static Blog.Comment.Reply CommentAdvancedSelectById(int commentAdvancedId)
        {
            Blog.Comment.Reply row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", commentAdvancedId);
              }, map: delegate (IDataReader reader, short set)
              {
                  Blog.Comment.Reply b = new Blog.Comment.Reply();
                  int startingIndex = 0;

                  b.ID = reader.GetSafeInt32(startingIndex++);
                  b.ParentCommentID = reader.GetSafeInt32(startingIndex++);
                  b.UserName = reader.GetSafeString(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                  b.DateModified = reader.GetSafeDateTime(startingIndex++);

                  if (row == null)
                  {
                      row = b;
                  }
              }
              );

            return row;
        } // CommentAdvancedSelectById

        public static List<Blog.Comment.Reply> CommentAdvancedSelectByParentCommentId(int ParentCommentID)
        {
            List<Blog.Comment.Reply> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectByParentCommentId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ParentCommentId", ParentCommentID);
              }, map: delegate (IDataReader reader, short set)
              {
                  Blog.Comment.Reply b = new Blog.Comment.Reply();
                  int startingIndex = 0;

                  b.ParentCommentID = reader.GetSafeInt32(startingIndex++);
                  b.UserName = reader.GetSafeString(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                  b.DateModified = reader.GetSafeDateTime(startingIndex++);

                  if (list == null)
                  {
                      list = new List<Blog.Comment.Reply>();
                  }

                  list.Add(b);
              }
              );

            return list;
        } // CommentAdvancedSelectByParentCommentId 

        public static void CommentAdvancedUpdate(int commentAdvancedId, string title, string content)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Update"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", commentAdvancedId);
                  paramCollection.AddWithValue("@Title", title);
                  paramCollection.AddWithValue("@Content", content);

              }, returnParameters: null
              );
        } // CommentAdvancedUpdate

        public static void CommentAdvancedDelete(int commentAdvancedId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ID", commentAdvancedId);
               }, returnParameters: null
               );

        } // CommentAdvancedDelete


    }
}