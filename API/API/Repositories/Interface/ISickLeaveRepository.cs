using API.Model;

namespace API.Repositories.Interface
{
    public interface ISickLeaveRepository
    {
        Task<List<SickLeave>> GetAllAsync();   
        Task<SickLeave> GetByIdAsync(int id);  
        Task<List<SickLeave>> GetExpiredSickLeavesAsync();  
        Task AddAsync(SickLeave sickLeave);
        Task UpdateAsync(SickLeave sickLeave);
        Task DeleteAsync(SickLeave sickLeave);  
    }
}
