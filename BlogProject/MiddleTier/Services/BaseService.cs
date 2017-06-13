using Dinh.Data.Providers;
using System.Data.SqlClient;

namespace Dinh.Mvc.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseService
    {
        protected static IDao DataProvider
        {

            get { return Dinh.Data.DataProvider.Instance; }
        }

        protected static SqlConnection GetConnection()
        {
            return new System.Data.SqlClient.SqlConnection(
                System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


        }
    }
}