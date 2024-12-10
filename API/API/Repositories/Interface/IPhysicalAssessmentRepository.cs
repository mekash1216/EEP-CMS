using API.Model;
using API.Model.DTO;

namespace API.Repositories.Interface
{
    public interface IPhysicalAssessmentRepository
    {
        Task<IEnumerable<PhysicalAssessment>> GetAllAsync();
        Task<PhysicalAssessment> GetByIdAsync(int id);
        Task<PhysicalAssessment> AddAsync(PhysicalAssessment physicalExamination);
        Task UpdateAsync(PhysicalAssessment physicalExamination);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<PhysicalAssessment>> GetByPatientIdAsync(int patientId);
    }
}
