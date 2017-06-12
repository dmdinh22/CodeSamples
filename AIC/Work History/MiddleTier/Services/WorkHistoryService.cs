using Aic.Data;
using Aic.Web.Domain;
using Aic.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aic.Web.Services
{
    public class WorkHistoryService : BaseService, IWorkHistoryService
    {

        public int WorkHistoryInsert(WorkHistoryAddRequest payload)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WorkHistory_Upsert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PersonID", payload.PersonID);
                    paramCollection.AddWithValue("@CompanyID", payload.CompanyID);
                    paramCollection.AddWithValue("@Company", payload.Company);
                    paramCollection.AddWithValue("@Title", payload.Title);
                    paramCollection.AddWithValue("@Contract", payload.Contract);
                    paramCollection.AddWithValue("@Datestarted", payload.DateStarted.ToShortDateString()); // .Date

                    if (payload.DateEnded.HasValue)
                    {
                        paramCollection.AddWithValue("@DateEnded", payload.DateEnded.Value.Date);
                    }
                    else
                    {
                        // send dbnull
                        paramCollection.AddWithValue("@DateEnded", System.DBNull.Value);
                    };

                    paramCollection.AddWithValue("@Description", payload.Description);

                    SqlParameter w = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                    w.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(w);
                }, returnParameters: delegate (SqlParameterCollection paramCollection)
                {
                    int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
                }
                );
            return id;
        } // WorkHistoryInsert

        public List<WorkHistory> WorkHistorySelectAll()
        {
            List<WorkHistory> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WorkHistory_SelectAll"
                , inputParamMapper: null, map: delegate (IDataReader reader, short set)
                {
                    WorkHistory w = Mapper(reader);

                    if (list == null)
                    {
                        list = new List<WorkHistory>();
                    }

                    list.Add(w);
                }
                );

            return list;
        } // WorkHistorySelectAll

        private static WorkHistory Mapper(IDataReader reader)
        {
            WorkHistory w = new WorkHistory();
            int startingIndex = 0;

            w.ID = reader.GetSafeInt32(startingIndex++);
            w.PersonID = reader.GetSafeInt32(startingIndex++);
            w.CompanyID = reader.GetSafeInt32(startingIndex++);
            w.Company = reader.GetSafeString(startingIndex++);
            w.Title = reader.GetSafeString(startingIndex++);
            w.Contract = reader.GetSafeBool(startingIndex++);
            w.DateStarted = reader.GetSafeDateTime(startingIndex++);
            w.DateEnded = reader.GetSafeDateTimeNullable(startingIndex++);
            w.Description = reader.GetSafeString(startingIndex++);
            w.ModifiedBy = reader.GetSafeGuidNullable(startingIndex++);
            w.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            return w;
        } //WorkHistory Mapper

        public WorkHistory WorkHistorySelectById(int workHistoryId)
        {
            WorkHistory row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WorkHistory_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", workHistoryId);
                }, map: delegate (IDataReader reader, short set)
                {
                    WorkHistory w = Mapper(reader);
                    if (row == null)
                    {
                        row = w;
                    }
                }
                );
            return row;
        } //WorkHistorySelectById

        public void WorkHistoryUpdate(WorkHistoryUpdateRequest payload)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WorkHistory_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", payload.ID);
                    paramCollection.AddWithValue("@PersonID", payload.PersonID);
                    paramCollection.AddWithValue("@CompanyID", payload.CompanyID);
                    paramCollection.AddWithValue("@Company", payload.Company);
                    paramCollection.AddWithValue("@Title", payload.Title);
                    paramCollection.AddWithValue("@Contract", payload.Contract);
                    paramCollection.AddWithValue("@DateStarted", payload.DateStarted.ToShortDateString());
                    if (payload.DateEnded.HasValue)
                    {
                        paramCollection.AddWithValue("@DateEnded", payload.DateEnded.Value.Date);
                    }
                    else
                    {
                        // send dbnull
                        paramCollection.AddWithValue("@DateEnded", System.DBNull.Value);
                    };
                    paramCollection.AddWithValue("@Description", payload.Description);
                }, returnParameters: null
                );
        }     //WorkHistoryUpdate

        //Select by USER ID
        public List<WorkHistory> WorkHistorySelectByUserId(int pid)
        {

            List<WorkHistory> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WorkHistory_SelectByPersonId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PersonId", pid);
                }
                , map: delegate (IDataReader reader, short set)
                {
                    WorkHistory w = Mapper(reader);

                    if (list == null)
                    {
                        list = new List<WorkHistory>();
                    }

                    list.Add(w);
                }
               );
            return list;
        }

        public void WorkHistoryDelete(int workHistoryId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WorkHistory_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", workHistoryId);
                }, returnParameters: null
                );
        } //WorkHistoryDelete

        public List<WorkHistory> WorkHistorySelectByPersonId(int id)
        {
            List<WorkHistory> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WorkHistory_SelectByPersonId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PersonId", id);
                }, map: delegate (IDataReader reader, short set)
                {
                    WorkHistory w = Mapper(reader);

                    if (list == null)
                    {
                        list = new List<WorkHistory>();
                    }

                    list.Add(w);
                }
                );
            return list;

        } // WorkHistorySelectByPersonId

    }
}