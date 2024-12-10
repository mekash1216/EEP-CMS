using API.Model;
using API.Model.DTO;

namespace API.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(int id);
        Task AddAsync(PatientDTO assignmentDTO);
       // Task AssignDoctorAsync(int patientId, int doctorId);
        Task UpdatePatientDataAsync(int patientId);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(int id);

        Task<IEnumerable<Patient>> GetPatientsByDayAsync(DateTime date);
        Task<IEnumerable<Patient>> GetPatientsByMonthAsync(int month, int year);
        Task<IEnumerable<Patient>> GetPatientsByYearAsync(int year);
    }
}

