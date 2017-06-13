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
    /// <summary>
    /// This class will handle all of the connections between the database and our
    /// project as it pertains to the Comment Replies
    /// </summary>
    public class CommentReplyService : BaseService
    {
        public static int CommentReplyInsert(int ParentCommentID, string author, string title, string content)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ParentCommentID", ParentCommentID);
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

        public static List<CommentAdvanced> CommentReplySelect()
        {
            List<CommentAdvanced> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_Select"
               , inputParamMapper: null, map: delegate (IDataReader reader, short set)
               {
                   CommentAdvanced b = new CommentAdvanced();
                   int startingIndex = 0;

                   b.ID = reader.GetSafeInt32(startingIndex++);
                   b.ParentCommentID = reader.GetSafeInt32(startingIndex++);
                   b.Author = reader.GetSafeString(startingIndex++);
                   b.Title = reader.GetSafeString(startingIndex++);
                   b.Content = reader.GetSafeString(startingIndex++);
                   b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                   b.DateModified = reader.GetSafeDateTime(startingIndex++);

                   if (list == null)
                   {
                       list = new List<CommentAdvanced>();
                   }

                   list.Add(b);
               }
               );


            return list;
        } //CommentReplySelect

        public static List<CommentAdvanced> CommentReplySelectByCommentId(int ParentCommentID)
        {
            List<CommentAdvanced> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectByCommentId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ParentCommentID", ParentCommentID);
              }, map: delegate (IDataReader reader, short set)
              {
                  CommentAdvanced b = new CommentAdvanced();
                  int startingIndex = 0;

                  b.ParentCommentID = reader.GetSafeInt32(startingIndex++);
                  b.Author = reader.GetSafeString(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.DateCreated = reader.GetSafeDateTime(startingIndex++);
                  b.DateModified = reader.GetSafeDateTime(startingIndex++);

                  if (list == null)
                  {
                      list = new List<CommentAdvanced>();
                  }

                  list.Add(b);
              }
              );

            return list;
        } // CommentReplySelectByBlogId 

        public static CommentAdvanced CommentReplySelectById(int commentReplyId)
        {
            CommentAdvanced row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", commentReplyId);
              }, map: delegate (IDataReader reader, short set)
              {
                  CommentAdvanced b = new CommentAdvanced();
                  int startingIndex = 0;

                  b.ID = reader.GetSafeInt32(startingIndex++);
                  b.ParentCommentID = reader.GetSafeInt32(startingIndex++);
                  b.Author = reader.GetSafeString(startingIndex++);
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
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Update"
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
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ID", commentReplyId);
               }, returnParameters: null
               );

        } // CommentReplyDelete
    }
}