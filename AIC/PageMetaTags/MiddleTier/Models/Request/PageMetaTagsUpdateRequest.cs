using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aic.Web.Models
{
    public class PageMetaTagsUpdateRequest
    {

        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string PageUrl { get; set; }
        public string Description { get; set; }
        public int OwnerTypeId { get; set; }
    }
}