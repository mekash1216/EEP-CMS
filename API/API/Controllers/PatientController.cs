using API.Model;
using API.Model.DTO;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientRepository patientRepository, IDoctorRepository doctorRepository, IMapper mapper,ILogger<PatientController> logger)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetAll()
        {
     
            var patients = await _patientRepository.GetAllAsync();

        
            var sortedPatients = patients.OrderByDescending(p => p.assigneddate).ToList();

            return Ok(_mapper.Map<IEnumerable<PatientDTO>>(sortedPatients));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetById(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(_mapper.Map<PatientDTO>(patient));
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignment(PatientDTO assignmentDTO)
        {
            await _patientRepository.AddAsync(assignmentDTO);
            return NoContent();
        }

        [HttpPut("updateData")]
        public async Task<ActionResult> UpdateData(UpdatePatientDataDTO updatePatientDataDto)
        {
            await _patientRepository.UpdatePatientDataAsync(updatePatientDataDto.PatientId);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PatientDTO patientDto)
        {
            if (id != patientDto.Id) return BadRequest();
            var patient = _mapper.Map<Patient>(patientDto);
            await _patientRepository.UpdateAsync(patient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _patientRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("daily/{date}")]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetByDay(DateTime date)
        {
            var patients = await _patientRepository.GetPatientsByDayAsync(date);
            return Ok(_mapper.Map<IEnumerable<PatientDTO>>(patients));
        }

        [HttpGet("monthly/{year}/{month}")]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetByMonth(int year, int month)
        {
            var patients = await _patientRepository.GetPatientsByMonthAsync(year, month);
            return Ok(_mapper.Map<IEnumerable<PatientDTO>>(patients));
        }

        [HttpGet("yearly/{year}")]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetByYear(int year)
        {
            var patients = await _patientRepository.GetPatientsByYearAsync(year);
            return Ok(_mapper.Map<IEnumerable<PatientDTO>>(patients));
        }
    }
}
