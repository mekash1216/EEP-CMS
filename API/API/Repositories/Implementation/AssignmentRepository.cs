namespace API.Repositories.Implementation;
using API.Model.DTO;
using API.Model;
using API.Repositories.Interface;
using AutoMapper;
using API.Data;
using Microsoft.EntityFrameworkCore;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public AssignmentRepository(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AssignmentDetailDTO>> GetAllAssignmentsAsync()
    {
        var assignments = await _context.Assignments
            .Include(a => a.Patient)
            //.Include(a => a.Doctor)
            
            .ToListAsync();

        return _mapper.Map<IEnumerable<AssignmentDetailDTO>>(assignments);
    }

    public async Task<AssignmentDetailDTO> GetAssignmentByIdAsync(int id)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Patient)
            //.Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.Id == id);

        return _mapper.Map<AssignmentDetailDTO>(assignment);
    }

    public async Task AddAssignmentAsync(AssignDTO assignmentDTO)
    {
        var assignment = _mapper.Map<Assignment>(assignmentDTO);
        await _context.Assignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAssignmentAsync(AssignmentDTO assignmentDTO)
    {
        var assignment = await _context.Assignments.FindAsync(assignmentDTO.Id);

        if (assignment != null)
        {
            assignment.PatientId = assignmentDTO.PatientId;
            //assignment.DoctorId = assignmentDTO.DoctorId;
            assignment.AssignedDate = assignmentDTO.AssignedDate;
            assignment.Weight = assignmentDTO.Weight;
            assignment.SystolicPressure = assignmentDTO.SystolicPressure;
            assignment.DiastolicPressure = assignmentDTO.DiastolicPressure;
            assignment.RespiratoryRate = assignmentDTO.RespiratoryRate;
            assignment.PulseRate = assignmentDTO.PulseRate;
            assignment.Temperature = assignmentDTO.Temperature;
            assignment.SpO2 = assignmentDTO.SpO2;
            assignment.Triage = assignmentDTO.Triage;

            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteAssignmentAsync(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment == null)
        {
            throw new KeyNotFoundException("Assignment not found");
        }

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
    }
}

