using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aic.Web.Domain
{
    public class ReferralPending
    {
        public int ReferrerId { get; set; }
        public int CandidateId { get; set; }
        public int JobId { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string CandidateName { get; set; }
        public string CandidateGuid { get; set; }
    }
}