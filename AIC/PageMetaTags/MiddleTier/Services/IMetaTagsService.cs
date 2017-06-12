using System.Collections.Generic;
using Aic.Web.Domain;
using Aic.Web.Models;
using Aic.Web.Models.Requests;

namespace Aic.Web.Services
{
    public interface IMetaTagsService
    {
        List<MetaTags> MetaTagsSelectAll();
        void PageMetaTagsDelete(int id);
        void PageMetaTagsInsert(PageMetaTagsAddRequest payload); //int mtId,
        List<PageMetaTags> PageMetaTagsSelectAll();
        PageMetaTags PageMetaTagsSelectById(int id);
        void PageMetaTagsUpdate(PageMetaTagsUpdateRequest payload);
        List<PageMetaTags> PageMetaTags_SelectAllByOwnerName(string ownerName);
        List<OwnerType> OwnerType_SelectAll();
        int OwnerTypeInsert(OwnerTypeAddRequest payload);
        void OwnerTypeDelete(int id);
        List<SinglePageMetaTags> SinglePageMetaTags_SelectAllByOwnerType(string name);
        List<SinglePageMetaTags> SinglePageMetaTags_SelectAllByOwnerTypeId(int id);
    }
}