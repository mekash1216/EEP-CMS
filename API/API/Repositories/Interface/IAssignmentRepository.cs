using API.Model.DTO;

namespace API.Repositories.Interface
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<AssignmentDetailDTO>> GetAllAssignmentsAsync();
        Task<AssignmentDetailDTO> GetAssignmentByIdAsync(int id);
        Task AddAssignmentAsync(AssignDTO assignmentDTO);
        Task UpdateAssignmentAsync(AssignmentDTO assignmentDTO);
        Task DeleteAssignmentAsync(int id);
    }
}
