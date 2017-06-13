using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dinh.Mvc.Controllers.Api
{
    public class UsersApiController : BaseApiController
    {
        public void T()
        {
            
            DataProvider.ExecuteCmd(GetConnection, "dbo.Countries_SelectAll", inputParamMapper: null,
                map: delegate (IDataReader reader, short set)
            {
                Country c = new Country(); int startingIndex = 0; //startingOrdinal
                c.Id = reader.GetSafeInt32(startingIndex++);
                if (list == null)
                {
                    list = new List<Country>();
                }
                list.Add(c);
            }
            );
            
        }
    }
}
