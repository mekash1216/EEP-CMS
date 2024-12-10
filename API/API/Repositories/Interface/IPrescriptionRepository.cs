using API.Model;
using API.Model.DTO;

namespace API.Repositories.Interface
{
    public interface IPrescriptionRepository
    {
        Task Add(Prescription prescription);
        Task<Prescription> GetById(Guid id);
        Task Update(Prescription prescription);
        Task SaveChanges();
        Task<IEnumerable<PrescriptionDto>> GetAll(); 
        void Remove(Prescription prescription);
        Task<IEnumerable<PrescriptionDto>> GetPrescriptionsByPatientAsync(int patientId);
        Task<List<ReportDTO>> GetPrescriptionReportsAsync();
    }
}
