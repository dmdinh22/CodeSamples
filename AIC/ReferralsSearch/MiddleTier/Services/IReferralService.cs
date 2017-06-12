using aic.Web.Domain;
using aic.Web.Models.Requests;
using System.Collections.Generic;

namespace aic.Web.Services
{
    public interface IReferralService
    {
        int ReferralUpdateInsert(ReferralRequest payload);
        List<ReferralPending> GetPendingReferral(int referrerId, int candidateId, int jobId);
        List<ReferralsJob> GetMyReferrals(int ID);
    }
}