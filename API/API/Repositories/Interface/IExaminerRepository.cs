using API.Model.DTO;

namespace API.Repositories.Interface
{
    public interface IExaminerRepository
    {
        Task<IEnumerable<ExaminerDetailDto>> GetAllAssignmentsAsync();
        Task<ExaminerDetailDto> GetAssignmentByIdAsync(int id);
        Task AddAssignmentAsync(ExaminerDto ExaminerDto);
        Task UpdateAssignmentAsync(ExaminerDto ExaminerDto);
        Task DeleteAssignmentAsync(int id);
    }
}
