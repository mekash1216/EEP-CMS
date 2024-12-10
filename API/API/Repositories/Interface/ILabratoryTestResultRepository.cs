using API.Model.DTO;
using API.Model;

namespace API.Repositories.Interface
{
    public interface ILabratoryTestResultRepository
    {
        Task<IEnumerable<LaboratoryTestResult>> GetTestResultsByPatientIdAsync(int patientId);
        Task<LaboratoryTestResult> GetTestResultByIdAsync(int id);
        Task<LaboratoryTestResult> CreateTestResultAsync(LaboratoryTestResult testResult);
        Task<LaboratoryTestResult> UpdateTestResultAsync(LaboratoryTestResult testResult);
        Task DeleteTestResultAsync(int id);
        Task<LaboratoryTestResultDto> ProcessTestResultAsync(LaboratoryTestResult testResult);
        Task<LaboratorySubTestResultDto> ProcessSubTestResultAsync(LaboratorySubTestResult subTestResult);
        Task<IEnumerable<LaboratoryTestResult>> GetAllTestResultsAsync();
      
    }
}
