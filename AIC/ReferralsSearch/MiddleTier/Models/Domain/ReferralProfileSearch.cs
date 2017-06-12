using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aic.Web.Domain
{
    public class ReferralProfileSearch
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyID { get; set; }
        public string Company { get; set; }
        public string School { get; set; }
        public string ComplexUserId { get; set; }
        public string FileUrl { get; set; }
        public int TotalRows { get; set; }
    }
}