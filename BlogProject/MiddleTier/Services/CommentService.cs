using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dinh.Mvc.Domain.Blogs;
using Dinh.Data;
using Dinh.Mvc.Domain;

namespace Dinh.Mvc.Services
{
    /// <summary>
    /// This class will handle all of the connections between the database and our 
    /// project as it pertains to the Comments.
    /// </summary>
    public class CommentService : BaseService
    {
        public static int CommentInsert(int blogPostId, int? parentCommentId, string author, string title, string content)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@BlogPostId", blogPostId);
                   paramCollection.AddWithValue("@Author", author);
                   paramCollection.AddWithValue("@Title", title);
                   paramCollection.AddWithValue("@Content", content);

                   SqlParameter p = paramCollection.Add("@ParentCommentId", System.Data.SqlDbType.Int);
                   if (parentCommentId == null)
                       p.Value = System.DBNull.Value;
                   else
                       p.Value = parentCommentId;

                   SqlParameter c = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                   c.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(c);

                   //paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection paramCollection)
               {
                   int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
               }
               );
            return id;
        } // CommentInsert

        public static List<CommentAdvanced> CommentSelect()
        {
            List<CommentAdvanced> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_Select"
               , inputParamMapper: null, map: delegate (IDataReader reader, short set)
               {
                   CommentAdvanced b = new CommentAdvanced();
                   int startingIndex = 0;

                   b.ID = reader.GetSafeInt32(startingIndex++);
                   b.BlogPostID = reader.GetSafeInt32(startingIndex++);
                   b.Title = reader.GetSafeString(startingIndex++);
                   b.Content = reader.GetSafeString(startingIndex++);
                   b.Author = reader.GetSafeString(startingIndex++);
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
        } //CommentSelect

        public static List<CommentAdvanced> CommentSelectByBlogId(int blogPostId)
        {
            List<CommentAdvanced> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectByBlogId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@BlogPostId", blogPostId);
              }, map: delegate (IDataReader reader, short set)
              {
                  CommentAdvanced b = new CommentAdvanced();
                  int startingIndex = 0;

                  b.BlogPostID = reader.GetSafeInt32(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.Author = reader.GetSafeString(startingIndex++);
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
        } // CommentSelectByBlogId -- replaced by GetCommentsByBlogId in BlogsAPIController

        public static CommentAdvanced CommentSelectById(int id)
        {
            CommentAdvanced row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CommentAdvanced_SelectById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", id);
              }, map: delegate (IDataReader reader, short set)
              {
                  CommentAdvanced b = new CommentAdvanced();
                  int startingIndex = 0;

                  b.ID = reader.GetSafeInt32(startingIndex++);
                  b.BlogPostID = reader.GetSafeInt32(startingIndex++);
                  b.ParentCommentID = reader.GetSafeInt32Nullable(startingIndex++);
                  b.Title = reader.GetSafeString(startingIndex++);
                  b.Content = reader.GetSafeString(startingIndex++);
                  b.Author = reader.GetSafeString(startingIndex++);
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

        public static void CommentUpdate(int id, string title, string content)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Update"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", id);
                  paramCollection.AddWithValue("@Title", title);
                  paramCollection.AddWithValue("@Content", content);

              }, returnParameters: null
              );
        } // CommentUpdate

        public static void CommentDelete(int ParentCommentID)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CommentAdvanced_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ID", ParentCommentID);
               }, returnParameters: null
               );

        } // CommentDelete

    }
}