using Dinh.Data;
using Dinh.Mvc.Domain.Blogs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CL = Dinh.Mvc.Classes;
using Dinh.Mvc.Domain;
using System.Data;

namespace Dinh.Mvc.Services.Blogs
{
    public class BlogService : BaseService
    {

        public static List<CommentAdvanced> BlogCommentSelect(int BlogPostId)
        {
            List<CommentAdvanced> list = null;


            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectByBlogId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  SqlParameter parameter = paramCollection.Add("@BlogPostId", SqlDbType.Int);
                  parameter.Value = BlogPostId;
              }, map: delegate (IDataReader reader, short set)
              {
                  CommentAdvanced temp = BindBlogComment(reader);

                  // Now, we have fully hydrated our temp model
                  if (list == null)
                  {
                      list = new List<CommentAdvanced>();
                  }

                  // Add the temp model into the resulting list.
                  list.Add(temp);
              }
              );

            return list;

        }

        private static List<CommentAdvanced> BlogCommentSelectReplies(int ParentCommentId)
        {
            List<CommentAdvanced> list = null;


            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectByParentCommentId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  SqlParameter parameter = paramCollection.Add("@ParentCommentId", SqlDbType.Int);
                  parameter.Value = ParentCommentId;
              }, map: delegate (IDataReader reader, short set)
              {
                  CommentAdvanced temp = BindBlogComment(reader);

                  // Now, we have fully hydrated our temp model
                  if (list == null)
                  {
                      list = new List<CommentAdvanced>();
                  }

                  // Add the temp model into the resulting list.
                  list.Add(temp);
              }
              );

            return list;

        }

        private static CommentAdvanced BindBlogComment(IDataReader reader)
        {
            CommentAdvanced temp = new CommentAdvanced();
            int startingIndex = 0; //startingOrdinal

            temp.ID = reader.GetSafeInt32(startingIndex++);
            temp.BlogPostID = reader.GetSafeInt32(startingIndex++);
            temp.ParentCommentID = reader.GetSafeInt32(startingIndex++);
            temp.Author = reader.GetSafeString(startingIndex++);
            temp.Title = reader.GetSafeString(startingIndex++);
            temp.Content = reader.GetSafeString(startingIndex++);
            temp.DateCreated = reader.GetSafeDateTime(startingIndex++);
            temp.DateModified = reader.GetSafeDateTime(startingIndex++);

            // Get all of our children
            temp.Replies = BlogCommentSelectReplies(temp.ID);

            return temp;

        }

        public static int BlogInsert(string title, string content, int authorId)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Blog_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Title", title);
                   paramCollection.AddWithValue("@Content", content);
                   paramCollection.AddWithValue("@AuthorId", authorId);

                   SqlParameter p = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection paramCollection)
               {
                   int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
               }
               );
            return id;
        } // BlogInsert


        public static List<Blog> BlogSelect()
        {
            List<Blog> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Blog_Select"
               , inputParamMapper: null, map: delegate (IDataReader reader, short set)
               {
                   Blog b = new Blog();
                   int startingIndex = 0; //startingOrdinal

                   b.ID = reader.GetSafeInt32(startingIndex++);
                   b.Title = reader.GetSafeString(startingIndex++);
                   b.Content = reader.GetSafeString(startingIndex++);
                   b.AuthorID = reader.GetSafeInt32(startingIndex++);
                   b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                   b.DateModified = reader.GetSafeDateTime(startingIndex++); 

                   if (list == null)
                   {
                       list = new List<Blog>();
                   }

                   list.Add(b);
               }
               );


            return list;
        } //BlogSelect

        public static Blog BlogSelectById(int blogId)
        {
            Blog row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Blog_SelectById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", blogId);
              }, map: delegate (IDataReader reader, short set)
              {
                  Blog b = new Blog();
                  int startingIndex = 0; //startingOrdinal

                  b.ID = reader.GetSafeInt32(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.AuthorID = reader.GetSafeInt32(startingIndex++);
                  b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                  b.DateModified = reader.GetSafeDateTime(startingIndex++);

                  if (row == null)
                  {
                      row = b;
                  }
              }
              );

            return row;
        } //BlogSelectById


        public static void BlogUpdate(int blogId, string title, string content, int authorId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Blog_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ID", blogId);
                   paramCollection.AddWithValue("@Title", title);
                   paramCollection.AddWithValue("@Content", content);
                   // paramCollection.AddWithValue("@AuthorId", authorId); -- not a parameter in Blog_Update
                   // DateModified is already set in Blog_Update.sql
               }, returnParameters: null
               );

        } // BlogUpdate

        public static void BlogDelete(int blogId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Blog_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ID", blogId);
               }, returnParameters: null
               );

        } // BlogDelete

        // public static Blog BlogSelectAuthorById(int id)
        public static Blog GetBlogAuthorById(int blogId)
        {
            return CL.Database.BlogSelectById(blogId);
        }

        //  public static List<Blog.Comment> CommentSelectByBlogId(int blogID)
        public static List<Blog.Comment> GetBlogCommentsById(int blogId)
        {
            return CL.Database.CommentSelectByBlogId(blogId);
        }

        // CommentInsert(int blogID, string title, string text, string createdUser)
        public static int CommentInsert(int blogPostID, string Title, string Content, string UserName)
        {
            return CL.Database.CommentInsert(blogPostID, Title, Content, UserName);
        }

        // CommentUpdate(int id, string title, string text)
        public static void CommentUpdate(int commentId, string title, string content)
        {
            CL.Database.CommentUpdate(commentId, title, content);
        }

        // CommentDelete(int id)
        public static void CommentDelete(int commentId)
        {
            CL.Database.CommentDelete(commentId);
        }

        // CommentSelectById(int id)
        public static Blog.Comment CommentSelectById(int commentId)
        {
            return CL.Database.CommentSelectById(commentId);
        }
    }
}