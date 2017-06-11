using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aic.Web.Domain
{
    public class ReferralsJob
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLocation { get; set; }
        public string Type { get; set; }
        public int Salary { get; set; }
        public List<ReferralsPerson> ReferralsPerson { get; set; }
    }
}