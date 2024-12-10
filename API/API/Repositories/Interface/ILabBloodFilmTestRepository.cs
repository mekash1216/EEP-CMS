using API.Model;

namespace API.Repositories.Interface
{
    public interface ILabBloodFilmTestRepository
    {
        Task<LabBloodFilmTest> GetBloodFilmTestByIdAsync(int id);
        Task<IEnumerable<LabBloodFilmTest>> GetAllBloodFilmTestsAsync();
        Task<LabBloodFilmTest> CreateBloodFilmTestAsync(LabBloodFilmTest bloodFilmTest);
        Task<LabBloodFilmTest> UpdateBloodFilmTestAsync(LabBloodFilmTest bloodFilmTest);
        Task DeleteBloodFilmTestAsync(int id);
        Task<IEnumerable<LabBloodFilmTest>> GetBloodFilmTestsByPatientIdAsync(int patientId);
    }
}
