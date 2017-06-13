using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dinh.Data;
using Dinh.Mvc.Domain.Blogs;
using Dinh.Mvc.Domain;

namespace Dinh.Mvc.Services
{
    public class LegacyCommentReplyService
    {
        
        public static int CommentReplyInsert(int commentId, string author, string title, string content)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentReply_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@CommentId", commentId);
                   paramCollection.AddWithValue("@Author", author);
                   paramCollection.AddWithValue("@Title", title);
                   paramCollection.AddWithValue("@Content", content);


                   SqlParameter p = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection paramCollection)
               {
                   int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
               }
               );
            return id;
        } // CommentReplyInsert

        public static List<Blog.Comment.Reply> CommentReplySelect()
        {
            List<Blog.Comment.Reply> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentReply_Select"
               , inputParamMapper: null, map: delegate (IDataReader reader, short set)
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

                   if (list == null)
                   {
                       list = new List<Blog.Comment.Reply>();
                   }

                   list.Add(b);
               }
               );


            return list;
        } //CommentReplySelect

        public static List<Blog.Comment.Reply> CommentReplySelectByCommentId(int commentId)
        {
            List<Blog.Comment.Reply> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentReply_SelectByCommentId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@CommentId", commentId);
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
        } // CommentReplySelectByBlogId 

        public static Blog.Comment.Reply CommentReplySelectById(int commentReplyId)
        {
            Blog.Comment.Reply row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentReply_SelectById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", commentReplyId);
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
        } // CommentReplySelectById

        public static void CommentReplyUpdate(int commentReplyId, string title, string content)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentReply_Update"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", commentReplyId);
                  paramCollection.AddWithValue("@Title", title);
                  paramCollection.AddWithValue("@Content", content);

              }, returnParameters: null
              );
        } // CommentReplyUpdate

        public static void CommentReplyDelete(int commentReplyId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentReply_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ID", commentReplyId);
               }, returnParameters: null
               );

        } // CommentReplyDelete
        
    }
}