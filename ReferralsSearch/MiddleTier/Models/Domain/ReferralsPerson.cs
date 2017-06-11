using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aic.Web.Domain
{
    public class ReferralsPerson
    {
        public int ID { get; set; }
        public string FileUrl { get; set; }
        public int ReferrerId { get; set; }
        public bool Accepted { get; set; }
        public string AcceptedString
        {
            get
            {
                return Accepted ? "Accepted" : "Pending";
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}