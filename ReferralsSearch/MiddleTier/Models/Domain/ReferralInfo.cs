using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aic.Web.Domain
{
    public class ReferralInfo
    {
        public string Message { get; set; }

        public int ReferrerId { get; set; }

        public string ReferrerGuid { get; set; }

        public string ReferrerFirstName { get; set; }

        public string ReferrerLastName { get; set; }

        public bool Accepted { get; set; }

        public bool Hidden { get; set; }

        public bool UserNotified { get; set; }
    }
}