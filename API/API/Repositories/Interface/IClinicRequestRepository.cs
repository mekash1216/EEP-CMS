using API.Model;

namespace API.Repositories.Interface
{
    public interface IClinicRequestRepository
    {
        Task<ClinicRequest> GetByIdAsync(int id);
        Task<IEnumerable<ClinicRequest>> GetAllAsync();
        Task AddAsync(ClinicRequest request);
        Task UpdateAsync(ClinicRequest request);
        Task DeleteAsync(int id);


        Task<Patient> GetPatientByCardNoAsync(string cardNo);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
    }
}
