using API.Data;
using API.Model;
using API.Model.DTO;
using API.Repositories;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabBloodFilmTestController : ControllerBase
    {
        private readonly ILabBloodFilmTestRepository _repository;
        private readonly IMapper _mapper;

        public LabBloodFilmTestController(ILabBloodFilmTestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBloodFilmTest(int id)
        {
            var bloodFilmTest = await _repository.GetBloodFilmTestByIdAsync(id);
            if (bloodFilmTest == null)
                return NotFound();
            return Ok(_mapper.Map<LabBloodFilmTestDto>(bloodFilmTest));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBloodFilmTests()
        {
            var bloodFilmTests = await _repository.GetAllBloodFilmTestsAsync();
            return Ok(_mapper.Map<IEnumerable<LabBloodFilmTestDto>>(bloodFilmTests));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBloodFilmTest(LabBloodFilmTestDto bloodFilmTestDto)
        {
            var bloodFilmTest = _mapper.Map<LabBloodFilmTest>(bloodFilmTestDto);
            var createdBloodFilmTest = await _repository.CreateBloodFilmTestAsync(bloodFilmTest);
            return CreatedAtAction(nameof(GetBloodFilmTest), new { id = createdBloodFilmTest.Id }, _mapper.Map<LabBloodFilmTestDto>(createdBloodFilmTest));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBloodFilmTest(int id, LabBloodFilmTestDto bloodFilmTestDto)
        {
            if (id != bloodFilmTestDto.Id)
                return BadRequest();

            var bloodFilmTest = _mapper.Map<LabBloodFilmTest>(bloodFilmTestDto);
            await _repository.UpdateBloodFilmTestAsync(bloodFilmTest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodFilmTest(int id)
        {
            await _repository.DeleteBloodFilmTestAsync(id);
            return NoContent();
        }
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetBloodFilmTestsByPatientId(int patientId)
        {
            var bloodFilmTests = await _repository.GetBloodFilmTestsByPatientIdAsync(patientId);
            if (bloodFilmTests == null || !bloodFilmTests.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<LabBloodFilmTestDto>>(bloodFilmTests));
        }

    }
}
