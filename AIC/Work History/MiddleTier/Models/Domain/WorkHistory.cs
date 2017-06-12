using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aic.Web.Domain
{
    public class WorkHistory
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int CompanyID { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public bool Contract { get; set; }
        public string ContractString { 
            get { return Contract ? "Yes" : "No"; }
             }
        public DateTime DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }
        public string Description { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}