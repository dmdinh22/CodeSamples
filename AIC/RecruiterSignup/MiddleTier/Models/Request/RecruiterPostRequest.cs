using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aic.Web.Models.Requests.Recruiter
{
    public class RecruiterPostRequest
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyDescription { get; set; }
        [Required]
        public string CompanyAddress1 { get; set; }

        public string CompanyAddress2 { get; set; }
        [Required]
        public string CompanyCity { get; set; }
        [Required]
        public string CompanyState { get; set; }
        [Required]
        public string CompanyZip { get; set; }
        
        public string CountryCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        
        public string Extension { get; set; }
        [Required]
        public int PhoneType { get; set; }
        [Required]
        public string CompanyWebsite { get; set; }
        
        public string CompanyLogoUrl { get; set; }
        [Required]
        public string CompanyEmail { get; set; }
        [Required]
        public string RecruiterType { get; set; }

    }
}