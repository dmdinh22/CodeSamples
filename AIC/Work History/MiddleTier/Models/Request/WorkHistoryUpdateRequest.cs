using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aic.Web.Models.Requests
{
    public class WorkHistoryUpdateRequest
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int PersonID { get; set; }
        public int CompanyID { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Title { get; set; }
        public bool Contract { get; set; }
        public string ContractString { 
            set { this.Contract = value == "Yes" ? true : false; }
             }
        [Required]
        public DateTime DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }
        [Required]
        public string Description { get; set; }

    }
}