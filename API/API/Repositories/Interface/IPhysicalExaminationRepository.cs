using System.Collections.Generic;
using System.Threading.Tasks;
using API.Model;

namespace API.Repositories
{
    public interface IPhysicalExaminationRepository
    {
        Task<IEnumerable<PhysicalExamination>> GetAllAsync();
        Task<PhysicalExamination> GetByIdAsync(int id);
        Task<PhysicalExamination> AddAsync(PhysicalExamination physicalExamination);
        Task UpdateAsync(PhysicalExamination physicalExamination);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<PhysicalExamination>> GetByPatientIdAsync(int patientId);
    }

}
