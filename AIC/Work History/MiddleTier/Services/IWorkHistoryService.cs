using System.Collections.Generic;
using Aic.Web.Domain;
using Aic.Web.Models.Requests;

namespace Aic.Web.Services
{
    public interface IWorkHistoryService
    {
        void WorkHistoryDelete(int workHistoryId);
        int WorkHistoryInsert(WorkHistoryAddRequest payload);
        List<WorkHistory> WorkHistorySelectAll();
        WorkHistory WorkHistorySelectById(int workHistoryId);
        void WorkHistoryUpdate(WorkHistoryUpdateRequest payload);
        List<WorkHistory> WorkHistorySelectByPersonId(int id);
        List<WorkHistory> WorkHistorySelectByUserId(int pid);

    }
}