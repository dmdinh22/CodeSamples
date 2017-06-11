using Aic.Data;
using Aic.Web.Domain;
using Aic.Web.Models;
using Aic.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aic.Web.Services
{
    public partial class MetaTagsService : BaseService, IMetaTagsService
    {

        #region Page Meta Tags Web Services for DB
        public void PageMetaTagsInsert(PageMetaTagsAddRequest payload) //int mtId,
        {
            // create a new datatable
            DataTable newMT = new DataTable();

            // create a new comlumns to match SQL
            DataColumn ID = new DataColumn("ID", typeof(int));
            DataColumn MetaTagID = new DataColumn("MetaTagID", typeof(int)); // SQL doesn't understand object types
            DataColumn Value = new DataColumn("Value", typeof(string));
            DataColumn OwnerId = new DataColumn("OwnerId", typeof(int));
            DataColumn OwnerTypeId = new DataColumn("OwnerTypeId", typeof(int));

            // add columns to data table
            newMT.Columns.Add(ID);
            newMT.Columns.Add(MetaTagID);
            newMT.Columns.Add(Value);
            newMT.Columns.Add(OwnerId);
            newMT.Columns.Add(OwnerTypeId);

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.PageMetaTags_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {

                    // have to hydrate table with info from List
                    foreach (PageMetaTags p in payload.MetaTags)
                    {
                        DataRow dr1 = newMT.NewRow();
                        //dr1[0] = ID;
                        dr1[1] = (int)p.MetaTagID;
                        dr1[2] = p.MetaTagValue;
                        dr1[3] = 15;
                        dr1[4] = payload.OwnerTypeId; //mtId;

                        newMT.Rows.Add(dr1);
                    }

                    paramCollection.AddWithValue("@OwnerTypeId", payload.OwnerTypeId);
                    paramCollection.AddWithValue("@MetaTagTableType", newMT);
                }//, returnParameters: null
                );
        } // PageMetaTagsInsert

        public List<PageMetaTags> PageMetaTagsSelectAll()
        {
            List<PageMetaTags> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.PageMetaTags_SelectAll"
                , inputParamMapper: null, map: delegate (IDataReader reader, short set)
                {
                    PageMetaTags p = Mapper(reader);

                    if (list == null)
                    {
                        list = new List<PageMetaTags>();
                    }

                    list.Add(p);
                }
                );

            return list;
        } // PageMetaTagsSelectAll

        private static PageMetaTags Mapper(IDataReader reader)
        {
            PageMetaTags p = new PageMetaTags();
            int startingIndex = 0;

            p.ID = reader.GetSafeInt32(startingIndex++);
            p.MetaTagName = reader.GetSafeString(startingIndex++);
            p.MetaTagValue = reader.GetSafeString(startingIndex++);
            p.MetaTagID = (MtEnum)reader.GetSafeInt32(startingIndex++); // cast to Enum from int
            p.OwnerName = reader.GetSafeString(startingIndex++);
            p.OwnerTypeId = reader.GetSafeInt32(startingIndex++);

            return p;
        } //PageMetaTags Mapper

        public PageMetaTags PageMetaTagsSelectById(int id)
        {
            PageMetaTags row = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.PageMetaTags_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", id);
                }, map: delegate (IDataReader reader, short set)
                {
                    PageMetaTags p = Mapper(reader);
                    if (row == null)
                    {
                        row = p;
                    }
                }
                );
            return row;
        } //PageMetaTagsSelectById

        public void PageMetaTagsUpdate(PageMetaTagsUpdateRequest payload)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.PageMetaTags_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", payload.Title);
                    paramCollection.AddWithValue("@MetaTagName", payload.ImageUrl);
                    paramCollection.AddWithValue("@MetaTagValue", payload.PageUrl);
                    paramCollection.AddWithValue("@MetaTagID", payload.Description);
                    paramCollection.AddWithValue("@OwnerTypeId", payload.OwnerTypeId);
                }, returnParameters: null
                );
        } //PageMetaTagsUpdate

        public void PageMetaTagsDelete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.PageMetaTags_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", id);
                }, returnParameters: null
                );
        } //PageMetaTagsDelete

        public List<MetaTags> MetaTagsSelectAll()
        {
            List<MetaTags> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.MetaTags_SelectAll"
                , inputParamMapper: null, map: delegate (IDataReader reader, short set)
                {
                    MetaTags m = new MetaTags();
                    int startingIndex = 0;

                    m.ID = reader.GetSafeInt32(startingIndex++);
                    m.Name = reader.GetSafeString(startingIndex++);
                    m.Description = reader.GetSafeString(startingIndex++);
                    m.Enum = reader.GetSafeString(startingIndex++);

                    if (list == null)
                    {
                        list = new List<MetaTags>();
                    }

                    list.Add(m);
                }
                );

            return list;
        } // MetaTagsSelectAll

        public List<PageMetaTags> PageMetaTags_SelectAllByOwnerName(string ownerName)
        {
            //List<PageMetaTags> list = null;
            List<PageMetaTags> list = new List<PageMetaTags>(); //set default list to empty list

            // list will hydrate with data if available from DB below
            DataProvider.ExecuteCmd(GetConnection, "dbo.PageMetaTags_SelectAllByOwnerName"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@OwnerName", ownerName);
                }, map: delegate (IDataReader reader, short set)
                {
                    PageMetaTags m = new PageMetaTags();
                    int startingIndex = 0;

                    m.ID = reader.GetSafeInt32(startingIndex++);
                    m.MetaTagName = reader.GetSafeString(startingIndex++);
                    m.MetaTagValue = reader.GetSafeString(startingIndex++);
                    m.MetaTagID = (MtEnum)reader.GetSafeInt32(startingIndex++);
                    m.OwnerName = reader.GetSafeString(startingIndex++);
                    m.OwnerTypeId = reader.GetSafeInt32(startingIndex++);

                    list.Add(m);
                });
            // find everything within the list hydrated that has the ownerName equal to the ownerName param passed in
            List<PageMetaTags> newList = list.FindAll(obj => obj.OwnerName == ownerName);

            if (newList.Count > 0)
            {
                return newList;
            }
            else
            {
                return list;
            }

        } //PageMetaTags_SelectAllByOwnerName

        #endregion

        #region Owner Type Web Services for DB

        public List<OwnerType> OwnerType_SelectAll()
        {
            List<OwnerType> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.OwnerType_SelectAll"
                , inputParamMapper: null, map: delegate (IDataReader reader, short set)
                {
                    OwnerType o = new OwnerType();
                    int startingIndex = 0;

                    o.ID = reader.GetSafeInt32(startingIndex++);
                    o.Name = reader.GetSafeString(startingIndex++);
                    o.Description = reader.GetSafeString(startingIndex++);

                    if (list == null)
                    {
                        list = new List<OwnerType>();
                    }

                    list.Add(o);
                }
                );
            return list;
        } //OwnerType_SelectAll

        public int OwnerTypeInsert(OwnerTypeAddRequest payload)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.OwnerType_Insert"
            , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Name", payload.Name);
                paramCollection.AddWithValue("@Description", payload.Description);


                SqlParameter o = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                o.Direction = ParameterDirection.Output;

                paramCollection.Add(o);
            }, returnParameters: delegate (SqlParameterCollection paramCollection)
            {
                int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
            }
            );
            return id;
        } //OwnerTypeInsert

        public void OwnerTypeDelete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.OwnerType_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", id);
                }, returnParameters: null
                );
        } //OwnerTypeDelete
        #endregion

        #region Single Page Meta Tags Web Services

        public List<SinglePageMetaTags> SinglePageMetaTags_SelectAllByOwnerType(string name)
        {
            List<SinglePageMetaTags> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.PageMetaTags_ByOwnerType"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Name", name);
                }, map: delegate (IDataReader reader, short set)
                {
                    SinglePageMetaTags s = new SinglePageMetaTags();
                    int startingIndex = 0;

                    s.Value = reader.GetSafeString(startingIndex++);

                    if (list == null)
                    {
                        list = new List<SinglePageMetaTags>();
                    }

                    list.Add(s);
                });

            return list;
        } //SinglePageMetaTags_SelectAllByOwnerType

        public List<SinglePageMetaTags> SinglePageMetaTags_SelectAllByOwnerTypeId(int id)
        {
            List<SinglePageMetaTags> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.PageMetaTags_ByOwnerTypeId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@OwnerTypeId", id);
                }, map: delegate (IDataReader reader, short set)
                {
                    SinglePageMetaTags s = new SinglePageMetaTags();
                    int startingIndex = 0;

                    s.Name = reader.GetSafeString(startingIndex++);
                    s.Value = reader.GetSafeString(startingIndex++);

                    if (list == null)
                    {
                        list = new List<SinglePageMetaTags>();
                    }

                    list.Add(s);
                });

            return list;
        } //SinglePageMetaTags_SelectAllByOwnerTypeId
        #endregion
    }
}