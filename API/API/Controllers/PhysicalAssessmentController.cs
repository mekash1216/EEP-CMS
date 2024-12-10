using API.Model.DTO;
using API.Model;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Repositories.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalAssessmentController : ControllerBase
    {
        private readonly IPhysicalAssessmentRepository _repository;
        private readonly IMapper _mapper;

        public PhysicalAssessmentController(IPhysicalAssessmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalAssessmentDTO>>> GetAll()
        {
            var physicalExaminations = await _repository.GetAllAsync();
            var physicalExaminationsDto = _mapper.Map<IEnumerable<PhysicalAssessmentDTO>>(physicalExaminations);
            return Ok(physicalExaminationsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalAssessmentDTO>> GetById(int id)
        {
            var physicalExamination = await _repository.GetByIdAsync(id);
            if (physicalExamination == null)
            {
                return NotFound();
            }

            var physicalExaminationDto = _mapper.Map<PhysicalAssessmentDTO>(physicalExamination);
            return Ok(physicalExaminationDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PhysicalAssessmentDTO physicalExaminationDto)
        {
            var physicalExamination = _mapper.Map<PhysicalAssessment>(physicalExaminationDto);
            await _repository.AddAsync(physicalExamination);
            return CreatedAtAction(nameof(GetById), new { id = physicalExamination.Id }, physicalExaminationDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PhysicalAssessmentDTO physicalExaminationDto)
        {
            if (id != physicalExaminationDto.Id)
            {
                return BadRequest();
            }

            var physicalExamination = _mapper.Map<PhysicalAssessment>(physicalExaminationDto);
            await _repository.UpdateAsync(physicalExamination);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("byPatient/{patientId}")]
        public async Task<ActionResult<IEnumerable<PhysicalAssessment>>> GetByPatientId(int patientId)
        {
            var physicalExaminations = await _repository.GetByPatientIdAsync(patientId);
            var physicalExaminationsDto = _mapper.Map<IEnumerable<PhysicalAssessmentDTO>>(physicalExaminations);
            return Ok(physicalExaminationsDto);
        }

    }
}