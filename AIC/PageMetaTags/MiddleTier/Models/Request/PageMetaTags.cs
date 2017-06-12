using Aic.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aic.Web.Domain
{
    public class PageMetaTags
    {
        public int ID { get; set; }
        public string MetaTagName { get; set; }
        public string MetaTagValue { get; set; }
        public MtEnum MetaTagID { get; set; }
        public string OwnerName { get; set; }
        public int OwnerId { get; set; }
        public int OwnerTypeId { get; set; }

    }
}
