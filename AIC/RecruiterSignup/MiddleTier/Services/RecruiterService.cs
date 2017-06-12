using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Aic.Data;
using Aic.Web.Domain;
using Aic.Web.Models.Requests.Recruiter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Aic.Web.Services
{
    public class RecruiterService : BaseService
    {
        private static ApplicationUserManager GetUserManager()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public static void UpgradeToRecruiter()
        {
            User currentuser = UserService.UserSelect();

            ApplicationUserManager user = GetUserManager();

            string userGuid = HttpContext.Current.User.Identity.GetUserId();

            user.AddToRole(userGuid, "Recruiter");

        }

        public static void PostRecruiter(RecruiterPostRequest request)
        {
            string userGuid = UserService.GetCurrentUserId();
            Guid CreatedBy = new Guid(userGuid);
            Guid ModifiedBy = CreatedBy;
            User user = UserService.UserSelect();

            DataTable phonedt = new DataTable();
            phonedt.Columns.Add("PersonId", typeof(int));
            phonedt.Columns.Add("PhoneTypeID", typeof(int));
            phonedt.Columns.Add("CountryCode", typeof(string));
            phonedt.Columns.Add("Number", typeof(string));
            phonedt.Columns.Add("Extension", typeof(string));
            phonedt.Rows.Add(user.PersonId, request.PhoneType, request.CountryCode, request.PhoneNumber,
                request.Extension);

            DataTable addressdt = new DataTable();
            addressdt.Columns.Add("PersonId", typeof(int));
            addressdt.Columns.Add("Line1", typeof(string));
            addressdt.Columns.Add("Line2", typeof(string));
            addressdt.Columns.Add("City", typeof(string));
            addressdt.Columns.Add("State", typeof(string));
            addressdt.Columns.Add("PostalCode", typeof(string));
            addressdt.Columns.Add("Active", typeof(int));
            addressdt.Rows.Add(user.PersonId, request.CompanyAddress1, request.CompanyAddress2, request.CompanyCity, request.CompanyState, request.CompanyZip, "1");

            DataTable companydt = new DataTable();
            companydt.Columns.Add("Name", typeof(string));
            companydt.Columns.Add("Description", typeof(string));
            companydt.Columns.Add("WebsiteUrl", typeof(string));
            companydt.Columns.Add("LogoUrl", typeof(string));
            companydt.Columns.Add("DisplayOrder", typeof(int));
            companydt.Rows.Add(request.CompanyName, request.CompanyDescription, request.CompanyWebsite, request.CompanyLogoUrl, "1");

            DataTable recruiterdt = new DataTable();
            recruiterdt.Columns.Add("Email", typeof(string));
            recruiterdt.Columns.Add("Type", typeof(string));
            recruiterdt.Rows.Add(request.CompanyEmail, request.RecruiterType);

            DataProvider.ExecuteNonQuery(GetConnection,
                "dbo.Recruiter_InsertTableType"
                , inputParamMapper: delegate (SqlParameterCollection paramColletion)
                {
                    paramColletion.AddWithValue("@CreatedBy", userGuid);
                    paramColletion.AddWithValue("@PersonId", user.PersonId);
                    paramColletion.AddWithValue("@phonedt", phonedt);
                    paramColletion.AddWithValue("@addressdt", addressdt);
                    paramColletion.AddWithValue("@companydt", companydt);
                    paramColletion.AddWithValue("@recruiterdt", recruiterdt);

                });
        }

        public static List<JobInfo> GetJobWithReferrals(int id)
        {

            List<JobInfo> list = new List<JobInfo>();
            Dictionary<int, JobInfo> jobDictionary = new Dictionary<int, JobInfo>();
            Dictionary<string, JobReferral> jobReferralDictionary = new Dictionary<string, JobReferral>();
            List<JobReferral> referralList = new List<JobReferral>();
            List<ReferralInfo> referralInfo = new List<ReferralInfo>();

            DataProvider.ExecuteCmd(GetConnection,
                "dbo.ProductJob_Referral"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@personId", id);
                },
                map: delegate (IDataReader reader, short set)
                 {
                     JobInfo job = new JobInfo();
                     switch (set)
                     {
                         case 0:
                             JobInfo j = new JobInfo();
                             int startingIndex = 0;

                             j.Id = reader.GetSafeInt32(startingIndex++);
                             j.PersonId = reader.GetSafeInt32(startingIndex++);
                             j.CompanyId = reader.GetSafeInt32(startingIndex++);
                             j.CompanyName = reader.GetSafeString(startingIndex++);
                             j.CompanyLocation = reader.GetSafeString(startingIndex++);
                             j.CompanyIndustry = reader.GetSafeString(startingIndex++);
                             j.Title = reader.GetSafeString(startingIndex++);
                             j.Description = reader.GetSafeString(startingIndex++);
                             j.Type = reader.GetSafeString(startingIndex++);
                             j.Salary = reader.GetSafeInt32(startingIndex++);
                             j.isHighlighted = reader.GetSafeBool(startingIndex++);
                             j.isFeatured = reader.GetSafeBool(startingIndex++);
                             j.Referral = new List<JobReferral>();
                             list.Add(j);
                             jobDictionary.Add(j.Id, j);
                             break;

                         case 1:
                             JobReferral jr = new JobReferral();
                             int ord = 0;

                             jr.Id = reader.GetSafeInt32(ord++);
                             jr.JobId = reader.GetSafeInt32(ord++);
                             jr.CandidateId = reader.GetSafeInt32(ord++);
                             jr.CandidateGuid = reader.GetSafeString(ord++);
                             jr.CandidateFirstName = reader.GetSafeString(ord++);
                             jr.CandidateLastName = reader.GetSafeString(ord++);
                             jr.Favorite = reader.GetSafeBool(ord++);
                             string key = string.Format("{0}_{1}", jr.JobId, jr.CandidateId);
                             if (jobReferralDictionary.ContainsKey(key))
                             {
                                 ReferralInfo ri = new ReferralInfo();
                                 ri.Message = reader.GetSafeString(ord++);
                                 ri.ReferrerId = reader.GetSafeInt32(ord++);
                                 ri.ReferrerGuid = reader.GetSafeString(ord++);
                                 ri.ReferrerFirstName = reader.GetSafeString(ord++);
                                 ri.ReferrerLastName = reader.GetSafeString(ord++);
                                 ri.Accepted = reader.GetSafeBool(ord++);
                                 ri.Hidden = reader.GetSafeBool(ord++);
                                 ri.UserNotified = reader.GetSafeBool(ord++);


                                 jobReferralDictionary[key].ReferralInfo.Add(ri);
                             }
                             else
                             {
                                 if (jobDictionary.ContainsKey(jr.JobId))
                                 {
                                     jobDictionary[jr.JobId].Referral.Add(jr);
                                     jobReferralDictionary.Add(key, jr);
                                     
                                     ReferralInfo ri = new ReferralInfo();
                                     ri.Message = reader.GetSafeString(ord++);
                                     ri.ReferrerId = reader.GetSafeInt32(ord++);
                                     ri.ReferrerGuid = reader.GetSafeString(ord++);
                                     ri.ReferrerFirstName = reader.GetSafeString(ord++);
                                     ri.ReferrerLastName = reader.GetSafeString(ord++);
                                     ri.Accepted = reader.GetSafeBool(ord++);
                                     ri.Hidden = reader.GetSafeBool(ord++);
                                     ri.UserNotified = reader.GetSafeBool(ord++);
                                     if (jobReferralDictionary[key].ReferralInfo == null)
                                     {
                                         jobReferralDictionary[key].ReferralInfo = new List<ReferralInfo>();
                                     }
                                     jobReferralDictionary[key].ReferralInfo.Add(ri);
                                 }
                             }
                             break;

                     }
                 });
            return list;

        }

        public static List<Reminder> GetReminder()
        {
            List<Reminder> list = null;

            DataProvider.ExecuteCmd(GetConnection,
                "dbo.Reminder_SelectAll",
                inputParamMapper: null, map: delegate (IDataReader reader, short set)
                 {
                     Reminder r = new Reminder();
                     int startingIndex = 0;

                     r.ReminderId = reader.GetSafeInt32(startingIndex++);
                     r.ReminderDescription = reader.GetSafeString(startingIndex++);
                     r.ReminderDateTime = reader.GetSafeDateTime(startingIndex++);
                     r.ReminderDateTimeString = reader.GetSafeString(startingIndex++);
                     r.ReminderType = reader.GetSafeInt32(startingIndex++);
                     r.CreatedBy = reader.GetSafeString(startingIndex++);
                     r.ModifiedBy = reader.GetSafeString(startingIndex++);
                     r.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                     r.ModifiedDate = reader.GetSafeDateTime(startingIndex++);

                     if (list == null)
                     {
                         list = new List<Reminder>();
                     }
                     list.Add(r);
                 });
            return list;
        }

        public static List<Reminder> GetReminderByUserId()
        {
            string userGuid = HttpContext.Current.User.Identity.GetUserId();

            List<Reminder> list = null;

            DataProvider.ExecuteCmd(GetConnection,
                "dbo.Reminder_SelectByUserId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@userGuid", userGuid);
                }, map: delegate (IDataReader reader, short set)
                {
                    Reminder r = new Reminder();
                    int startingIndex = 0;

                    r.ReminderId = reader.GetSafeInt32(startingIndex++);
                    r.ReminderDescription = reader.GetSafeString(startingIndex++);
                    r.ReminderDateTime = reader.GetSafeDateTime(startingIndex++);
                    r.ReminderDateTimeString = reader.GetSafeString(startingIndex++);
                    r.ReminderType = reader.GetSafeInt32(startingIndex++);
                    r.CreatedBy = reader.GetSafeString(startingIndex++);
                    r.ModifiedBy = reader.GetSafeString(startingIndex++);
                    r.CreatedDate = reader.GetSafeDateTime(startingIndex++);
                    r.ModifiedDate = reader.GetSafeDateTime(startingIndex++);

                    if (list == null)
                    {
                        list = new List<Reminder>();
                    }
                    list.Add(r);
                });
            return list;
        }

        public static int PostReminder(ReminderPostRequest request)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection,
                "dbo.Reminder_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Description", request.ReminderDescription);
                    paramCollection.AddWithValue("@ReminderDateTime", request.ReminderDateTime);
                    paramCollection.AddWithValue("@ReminderDateTimeString", request.ReminderDateTimeString);
                    paramCollection.AddWithValue("@ReminderType", request.ReminderType);
                    paramCollection.AddWithValue("@CreatedBy", request.CreatedBy);
                    paramCollection.AddWithValue("@ModifiedBy", request.CreatedBy);

                    SqlParameter p = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);
                }, returnParameters: delegate (SqlParameterCollection paramCollection)
                {
                    int.TryParse(paramCollection["@ID"].Value.ToString(), out id);
                });

            return id;
        }

        public static void UpdateReminder(ReminderPutRequest request)
        {
            string userGuid = HttpContext.Current.User.Identity.GetUserId();
            Guid ModifiedBy = new Guid(userGuid);

            DataProvider.ExecuteNonQuery(GetConnection,
            "dbo.Reminder_Update"
            , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@ID", request.ReminderId);
                paramCollection.AddWithValue("@Description", request.ReminderDescription);
                paramCollection.AddWithValue("@ReminderDateTime", request.ReminderDateTime);
                paramCollection.AddWithValue("@ReminderDateTimeString", request.ReminderDateTimeString);
                paramCollection.AddWithValue("@ReminderType", request.ReminderType);
                paramCollection.AddWithValue("@ModifedBy", ModifiedBy);
            });
        }

        public static void DeleteReminder(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Reminder_Delete"
            , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@ID", id);

            });
        }
    }
}