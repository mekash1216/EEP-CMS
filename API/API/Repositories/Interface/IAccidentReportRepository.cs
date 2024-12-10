using API.Model;

namespace API.Repositories.Interface
{
    public interface IAccidentReportRepository
    {

        Task<AccidentReport> GetByIdAsync(int id);
        Task<IEnumerable<AccidentReport>> GetAllAsync();
        Task<AccidentReport> AddAsync(AccidentReport accidentReport);
        Task<AccidentReport> UpdateAsync(AccidentReport accidentReport);
        Task DeleteAsync(int id);
    }
}
