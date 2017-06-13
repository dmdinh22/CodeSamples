using Dinh.Data;
using Dinh.Mvc.Domain;
using Dinh.Mvc.Models.Requests.Blogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dinh.Mvc.Services
{
    /// <summary>
    /// This class will handle all of the connections between the database and our 
    /// project as it pertains to the Author.
    /// </summary>

    public class AuthorService : BaseService
    {
        public static int AuthorInsert(string email)
        {
            int id = 0;

            // Now we have the connection string to our database
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // We need to create a connection to that database.
            SqlConnection connection = new SqlConnection(connectionString);
            // we need to create a command
            SqlCommand command = new SqlCommand("dbo.Author_Insert", connection);
            // How long should we try to wait before detaching
            command.CommandTimeout = 30; // 0 = indefinite
            // what is our text, is it a stored procedure, or plain text?
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = command.Parameters.Add("@Email", SqlDbType.NVarChar);
            if (string.IsNullOrWhiteSpace(email))
            {
                // what do we know?
                // name is either "" or null
                parameter.Value = System.DBNull.Value;
            }
            else
            {
                parameter.Value = email;
            }
            // Once it getshere we no longer need the @Email parameter
            parameter = command.Parameters.Add("@ID", SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = System.DBNull.Value;

            // Open the connection
            connection.Open();

            // Execute the command.
            int recordsAffected = command.ExecuteNonQuery();

            //Let's console log the number of recoreds affected (for internal use)
            Console.WriteLine("{0} record(s) were affected", recordsAffected);

            // 1. Grab the parameter.
            SqlParameter outParameter = command.Parameters["@ID"];
            // checked to see if our parameter is null
            if (Convert.IsDBNull(outParameter.Value))
            {
                Console.WriteLine("Unable to assign ID");
            }
            else
            {
                // 3. We have retrieved the value
                string tempValue = outParameter.Value.ToString();

                // 4. We have tried to cast the value to an int
                if (int.TryParse(tempValue, out id))
                {
                    // We know once we are in here, that the id variable was set successfully.
                    Console.WriteLine("We successfully retrieved the ID.");
                }
                else
                {
                    // We know that he @ID parameter has some other value
                    Console.WriteLine("Unable to convert {0} to int.", tempValue);
                }
            }

            // Close our connection
            connection.Close();

            return id;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Author_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Email", email);

                    SqlParameter p = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);
                }, returnParameters: delegate (SqlParameterCollection paramCollection)
                {
                    int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
                }
                );

            return id;
        } // AuthorInsert

        public static Author AuthorSelectById(int id)
        {
            Author row = null;

            // Building up our connection
            SqlConnection connection = GetConnection();

            // Create a command.
            SqlCommand command = new SqlCommand("dbo.Authoer_SelectByID", connection);
            // determine the command type
            command.CommandType = CommandType.StoredProcedure;

            // Add our parameters
            SqlParameter parameter = command.Parameters.Add("@ID", SqlDbType.Int);

            parameter.Value = id;

            // Open our connection
            connection.Open();

            // execute the command
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Author tempAuthor = new Author();
                tempAuthor.ID = (int)reader["ID"];
                if (Convert.IsDBNull(reader["@Email"]) == false)
                {
                    tempAuthor.Email = reader["Email"].ToString();
                }
                tempAuthor.DateCreated = (DateTime)reader["DateCreated"];

                // Now, we have fully hydrated our temporary model.
                if (row == null)
                    row = tempAuthor;

            }

            // Close the data reader
            reader.Close();

            // Close the connection
            connection.Close();

            return row;


            DataProvider.ExecuteCmd(GetConnection, "dbo.Author_SelectById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", id);
              }, map: delegate (IDataReader reader2, short set)
              {
                  Author a = new Author();
                  int startingIndex = 0; //startingOrdinal

                  a.ID = reader2.GetSafeInt32(startingIndex++);
                  a.Email = reader2.GetSafeString(startingIndex++);
                  a.DateCreated = reader2.GetSafeDateTime(startingIndex++);

                  if (row == null)
                  {
                      row = a;
                  }
              }
              );

            return row;
        } // AuthorSelectById

        public static Author AuthorSelectByBlogId(int blogId)
        {
            Author row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Author_SelectByBlogId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  SqlParameter parameter = paramCollection.Add("@BlogID", SqlDbType.Int);
                  parameter.Value = blogId;
              }, map: delegate (IDataReader reader, short set)
              {
                  Author tempAuthor = new Author();
                  tempAuthor.ID = (int)reader["ID"];
                  if (Convert.IsDBNull(reader["Email"]) == false)
                  {
                      tempAuthor.Email = reader["Email"].ToString();
                  }
                  tempAuthor.DateCreated = (DateTime)reader["DateCreated"];

                  // Now, we have fully hydrated our temp model
                  if (row == null)
                  {
                      row = tempAuthor;
                  }
              }
              );

            return row;

        } //AuthorSelectByBlogId

        public static List<Author> AuthorSelect()
        {
            List<Author> list = null;

            // Build our connection
            SqlConnection connection = GetConnection();

            // Build our command
            SqlCommand command = new SqlCommand("dbo.Author_Select", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add our parameters.
            // no parmaeters, check stored procedures between "Create" and "As"

            //Open our connection
            connection.Open();

            // execute our command
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Author tempAuthor = new Author();
                tempAuthor.ID = (int)reader["ID"];
                if (Convert.IsDBNull(reader["Email"]) == false)
                {
                    tempAuthor.Email = reader["Email"].ToString();
                }
                tempAuthor.DateCreated = (DateTime)reader["DateCreated"];

                // Now, we have fully hydrated our temporary model.
                if (list == null)
                    list = new List<Author>();

                // Add the temp model into the resulting list.
                list.Add(tempAuthor);
            }

            // There are no more rows to add into the list.

            reader.Close();

            connection.Close();

            return list;


            DataProvider.ExecuteCmd(GetConnection, "dbo.Author_Select"
               , inputParamMapper: null, map: delegate (IDataReader reader2, short set)
               {
                   Author a = new Author();
                   int startingIndex = 0; //startingOrdinal

                   a.ID = reader2.GetSafeInt32(startingIndex++);
                   a.Email = reader2.GetSafeString(startingIndex++);
                   a.DateCreated = reader2.GetSafeDateTime(startingIndex++);

                   if (list == null)
                   {
                       list = new List<Author>();
                   }

                   list.Add(a);
               }
               );


            return list;
        } // AuthorSelect

        public static void AuthorUpdate(AuthorUpdateRequest payload)
        {
            AuthorUpdate(payload.ID, payload.Email);
        } //AuthorUpdate

        public static void AuthorUpdate(int id, string email)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Author_Update"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@ID", id);
                  paramCollection.AddWithValue("@Email", email);
              }, returnParameters: null
              );
        } //AuthorUpdate

        public static void AuthorDelete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Author_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ID", id);
               }, returnParameters: null
               );
        } // AuthorDelete
    }
}