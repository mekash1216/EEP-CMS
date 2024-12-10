using API.Model.DTO;
using API.Repositories.Interface;
using API.Repositories.Implementation;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;

        public AssignmentController(IAssignmentRepository assignmentRepository, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssignments()
        {
            var assignments = await _assignmentRepository.GetAllAssignmentsAsync();
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment);
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignment(AssignDTO assignmentDTO)
        {
            await _assignmentRepository.AddAssignmentAsync(assignmentDTO);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, AssignmentDTO assignmentDTO)
        {
            if (id != assignmentDTO.Id)
            {
                return BadRequest();
            }

            await _assignmentRepository.UpdateAssignmentAsync(assignmentDTO);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            await _assignmentRepository.DeleteAssignmentAsync(id);
            return NoContent();
        }
    }
}
