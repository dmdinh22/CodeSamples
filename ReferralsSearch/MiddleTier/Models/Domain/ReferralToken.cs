using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aic.Web.Domain
{
    public class ReferralToken
    {
        public string TokenId { get; set; }
        public string CandidateId { get; set; }
        public int JobId { get; set; }
        public string ReferreeEmail { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int PersonId { get; set; }
    }
}