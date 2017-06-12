using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aic.Web.Domain
{
    public class Referral
    {

        public string Title { get; set; }

        public string Type { get; set; }

        public int Salary { get; set; }

        public int CandidateId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Message { get; set; }

        public int ReferrerId { get; set; }

        public bool Accepted { get; set; }

    }
}