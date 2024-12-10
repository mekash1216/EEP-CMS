using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using API.Model.DTO;
using API.Model;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using API.Data;

[Route("api/[controller]")]
[ApiController]
public class ClinicRequestsController : ControllerBase
{
    private readonly IClinicRequestRepository _repository;
    private readonly IMapper _mapper;
    private readonly ApiContext _context;

    public ClinicRequestsController(IClinicRequestRepository repository, IMapper mapper,ApiContext context)
    {
        _repository = repository;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClinicRequestDto>>> GetAll()
    {
        var requests = await _repository.GetAllAsync();
        var requestDtos = _mapper.Map<IEnumerable<ClinicRequestDto>>(requests);
        return Ok(requestDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClinicRequestDto>> GetById(int id)
    {
        var request = await _repository.GetByIdAsync(id);
        if (request == null)
        {
            return NotFound();
        }
        var requestDto = _mapper.Map<ClinicRequestDto>(request);
        return Ok(requestDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ClinicRequestDto requestDto)
    {
        var request = _mapper.Map<ClinicRequest>(requestDto);
        await _repository.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = request.Id }, requestDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ClinicRequestDto requestDto)
    {
        if (id != requestDto.Id)
        {
            return BadRequest();
        }

        var existingRequest = await _repository.GetByIdAsync(id);
        if (existingRequest == null)
        {
            return NotFound();
        }

        // Map the updated values from DTO to the existing entity
        var updatedRequest = _mapper.Map<ClinicRequest>(requestDto);

        // Call the repository update method
        await _repository.UpdateAsync(updatedRequest);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var request = await _repository.GetByIdAsync(id);
        if (request == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("assign-patient/{requestId}")]
    public async Task<IActionResult> AssignPatient(int requestId)
    {
        try
        {
            // Fetch the Clinic Request
            var clinicRequest = await _repository.GetByIdAsync(requestId);

            if (clinicRequest == null)
            {
                return NotFound(new { message = "Clinic request not found." });
            }

            // Check if the Patient exists in the Patients Table
            var existingPatient = await _repository.GetPatientByCardNoAsync(clinicRequest.EmployeeId);

            if (existingPatient == null)
            {
                // Add a new Patient if not found
                var newPatient = new Patient
                {
                    cardNo = clinicRequest.EmployeeId,
                    firstName = clinicRequest.PatientFirstName,
                    lastName = clinicRequest.PatientLastName,
                    gender = clinicRequest.Gender,
                    dateOfBirth = clinicRequest.Birthdate,
                    phoneNumber = clinicRequest.PhoneNumber,
                    Position = clinicRequest.Position,
                    Workplace = clinicRequest.Workplace,
                    Email = clinicRequest.Email,
                    assigneddate=clinicRequest.RequestDate,
                    PatientDepartment=clinicRequest.PatientDepartment,
                    age=clinicRequest.age,
                };

                await _repository.AddPatientAsync(newPatient);
            }
            else
            {
                // Update the existing Patient record
                existingPatient.firstName = clinicRequest.PatientFirstName;
                existingPatient.lastName = clinicRequest.PatientLastName;
                existingPatient.gender = clinicRequest.Gender;
                existingPatient.dateOfBirth = clinicRequest.Birthdate;
                existingPatient.phoneNumber = clinicRequest.PhoneNumber;
                existingPatient.Position = clinicRequest.Position;
                existingPatient.Workplace = clinicRequest.Workplace;
                existingPatient.Email = clinicRequest.Email;
                existingPatient.assigneddate = clinicRequest.RequestDate;
                existingPatient.PatientDepartment=clinicRequest.PatientDepartment;
                existingPatient.age=clinicRequest.age;

                await _repository.UpdatePatientAsync(existingPatient);
            }

          
            clinicRequest.status = "Pending";
            clinicRequest.RequestDate = DateTime.UtcNow; 

            await _repository.UpdateAsync(clinicRequest);

            return Ok(new { message = "Patient assigned successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }




    [HttpPost("approve-by-cardno")]
    public async Task<IActionResult> ApproveRequestByCardNo([FromBody] string cardNo)
    {
        if (string.IsNullOrEmpty(cardNo))
        {
            return BadRequest(new { message = "Invalid card number." });
        }

        var request = await _context.ClinicRequests.FirstOrDefaultAsync(r => r.EmployeeId == cardNo);
        if (request == null)
        {
            return NotFound(new { message = "Request not found." });
        }

        request.status = "Accepted";
        _context.ClinicRequests.Update(request);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Request status updated to 'Accepted'." });
    }





    [HttpPost("reject-by-cardno")]
    public async Task<IActionResult> RejectRequestByCardNo([FromBody] string cardNo)
    {
        var request = await _context.ClinicRequests.FirstOrDefaultAsync(r => r.EmployeeId == cardNo);
        if (request == null)
        {
            return NotFound("Request not found.");
        }

        request.status = "Rejected";
        _context.ClinicRequests.Update(request);
        await _context.SaveChangesAsync();

        return Ok("Request status updated to 'Rejected'.");
    }





}
