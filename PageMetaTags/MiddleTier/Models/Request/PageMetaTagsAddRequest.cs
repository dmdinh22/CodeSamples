using Aic.Web.Domain;
using System.Collections.Generic;

namespace Aic.Web.Models.Requests
{
    public class PageMetaTagsAddRequest
    {
        public List<PageMetaTags> MetaTags { get; set; }
        public int OwnerTypeId { get; set; }
    }
}