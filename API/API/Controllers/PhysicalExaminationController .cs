using System.Collections.Generic;
using System.Threading.Tasks;
using API.Model;
using API.Model.DTO;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalExaminationsController : ControllerBase
    {
        private readonly IPhysicalExaminationRepository _repository;
        private readonly IMapper _mapper;

        public PhysicalExaminationsController(IPhysicalExaminationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalExaminationDto>>> GetAll()
        {
            var physicalExaminations = await _repository.GetAllAsync();
            var physicalExaminationsDto = _mapper.Map<IEnumerable<PhysicalExaminationDto>>(physicalExaminations);
            return Ok(physicalExaminationsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalExaminationDto>> GetById(int id)
        {
            var physicalExamination = await _repository.GetByIdAsync(id);
            if (physicalExamination == null)
            {
                return NotFound();
            }

            var physicalExaminationDto = _mapper.Map<PhysicalExaminationDto>(physicalExamination);
            return Ok(physicalExaminationDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PhysicalExaminationDto physicalExaminationDto)
        {
            var physicalExamination = _mapper.Map<PhysicalExamination>(physicalExaminationDto);
            await _repository.AddAsync(physicalExamination);
            return CreatedAtAction(nameof(GetById), new { id = physicalExamination.Id }, physicalExaminationDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PhysicalExaminationDto physicalExaminationDto)
        {
            if (id != physicalExaminationDto.Id)
            {
                return BadRequest();
            }

            var physicalExamination = _mapper.Map<PhysicalExamination>(physicalExaminationDto);
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
        public async Task<ActionResult<IEnumerable<PhysicalExaminationDto>>> GetByPatientId(int patientId)
        {
            var physicalExaminations = await _repository.GetByPatientIdAsync(patientId);
            var physicalExaminationsDto = _mapper.Map<IEnumerable<PhysicalExaminationDto>>(physicalExaminations);
            return Ok(physicalExaminationsDto);
        }

    }
}
