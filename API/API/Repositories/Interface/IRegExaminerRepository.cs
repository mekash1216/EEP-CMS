using API.Model;


namespace API.Repositories.Interface
{
    public interface IRegExaminerRepository
    {
        Task<IEnumerable<RegExaminer>> GetRegExaminersAsync();
        Task<RegExaminer> GetRegExaminerByIdAsync(int id);
        Task AddRegExaminerAsync(RegExaminer examiner);
        Task UpdateRegExaminerAsync(RegExaminer examiner);
        Task DeleteRegExaminerAsync(int id);
        Task<bool> RegExaminerExistsAsync(int id);
    }
}



