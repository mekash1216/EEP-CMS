using API.Model;

namespace API.Repositories.Interface
{
    public interface IUrineDipsticksTestRepository
    {
        Task<IEnumerable<UrineDipsticksTest>> GetAllAsync();
        Task<UrineDipsticksTest> GetByIdAsync(int id);
        Task<UrineDipsticksTest> AddAsync(UrineDipsticksTest test);
        Task<UrineDipsticksTest> UpdateAsync(UrineDipsticksTest test);
        Task<bool> DeleteAsync(int id);
    }
}
