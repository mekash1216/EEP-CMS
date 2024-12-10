using API.Model.DTO;
using API.Model;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccidentReportController : ControllerBase
    {
        private readonly IAccidentReportRepository _repository;
        private readonly IMapper _mapper;

        public AccidentReportController(IAccidentReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccidentReportDto>>> GetAllAccidentReports()
        {
            var accidentReports = await _repository.GetAllAsync();
            var accidentReportDtos = _mapper.Map<IEnumerable<AccidentReportDto>>(accidentReports);
            return Ok(accidentReportDtos);
        }


        // GET: api/AccidentReport/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AccidentReportDto>> GetAccidentReportById(int id)
        {
            var accidentReport = await _repository.GetByIdAsync(id);
            if (accidentReport == null)
                return NotFound();

            var accidentReportDto = _mapper.Map<AccidentReportDto>(accidentReport);
            return Ok(accidentReportDto);
        }

        // POST: api/AccidentReport
        [HttpPost]
        public async Task<ActionResult<AccidentReportCreateDto>> CreateAccidentReport([FromBody] AccidentReportCreateDto reportDto)
        {
            if (reportDto == null)
                return BadRequest("Invalid data.");

            var report = _mapper.Map<AccidentReport>(reportDto);
            var createdReport = await _repository.AddAsync(report);

            var createdReportDto = _mapper.Map<AccidentReportCreateDto>(createdReport);
            return CreatedAtAction(nameof(GetAccidentReportById), new { id = createdReportDto.Id }, createdReportDto);
        }

        // PUT: api/AccidentReport/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccidentReport(int id, [FromBody] AccidentReportCreateDto reportDto)
        {
            if (id != reportDto.Id)
                return BadRequest("ID mismatch.");

            var report = _mapper.Map<AccidentReport>(reportDto);
            await _repository.UpdateAsync(report);

            return NoContent();
        }

        // DELETE: api/AccidentReport/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccidentReport(int id)
        {
            var existingReport = await _repository.GetByIdAsync(id);
            if (existingReport == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
