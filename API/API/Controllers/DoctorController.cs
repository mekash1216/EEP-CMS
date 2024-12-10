using API.Model;
using API.Model.DTO;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorRepository doctorRepository, IMapper mapper, ILogger<DoctorController> logger)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAll()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<DoctorDTO>>(doctors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetById(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null) return NotFound();
            return Ok(_mapper.Map<DoctorDTO>(doctor));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDTO doctorDTO)
        {
            try
            {
                var doctor = new Doctor
                {
                    Name = doctorDTO.Name,
                    Specialty = doctorDTO.Specialization
                };

                await _doctorRepository.AddAsync(doctor);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating doctor. Inner exception: {InnerException}", ex.InnerException?.Message);
                return StatusCode(500, $"An error occurred while saving the entity changes. See the inner exception for details. Error: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DoctorDTO doctorDto)
        {
            if (id != doctorDto.Id) return BadRequest();
            var doctor = _mapper.Map<Doctor>(doctorDto);
            await _doctorRepository.UpdateAsync(doctor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _doctorRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
