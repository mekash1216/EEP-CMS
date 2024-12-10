using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IReferralRepository
    {
        Task<Referral> AddReferralAsync(Referral referral);
        Task<Referral> GetReferralByIdAsync(int id);
        Task<IEnumerable<Referral>> GetAllReferralsAsync();
        Task<bool> DeleteReferralAsync(int id);
        Task<Referral> GetReferralWithDetailsAsync(int examinerId);
        Task<Referral> UpdateReferralAsync(Referral referral);
        Task<IEnumerable<Referral>> GetByPatientId(int patientId);
    }


}
