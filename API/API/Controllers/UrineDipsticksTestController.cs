using API.Model.DTO;
using API.Model;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrineDipsticksTestController : ControllerBase
    {
        private readonly IUrineDipsticksTestRepository _repository;
        private readonly IMapper _mapper;

        public UrineDipsticksTestController(IUrineDipsticksTestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrineDipsticksTestDto>>> GetAll()
        {
            var tests = await _repository.GetAllAsync();
            var testDtos = _mapper.Map<IEnumerable<UrineDipsticksTestDto>>(tests);
            return Ok(testDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UrineDipsticksTestDto>> GetById(int id)
        {
            var test = await _repository.GetByIdAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            var testDto = _mapper.Map<UrineDipsticksTestDto>(test);
            return Ok(testDto);
        }

        [HttpPost]
        public async Task<ActionResult<UrineDipsticksTestDto>> Create(UrineDipsticksTestDto testDto)
        {
            var test = _mapper.Map<UrineDipsticksTest>(testDto);
            var createdTest = await _repository.AddAsync(test);
            var createdTestDto = _mapper.Map<UrineDipsticksTestDto>(createdTest);
            return CreatedAtAction(nameof(GetById), new { id = createdTestDto.Id }, createdTestDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UrineDipsticksTestDto>> Update(int id, UrineDipsticksTestDto testDto)
        {
            if (id != testDto.Id)
            {
                return BadRequest();
            }

            var test = _mapper.Map<UrineDipsticksTest>(testDto);
            var updatedTest = await _repository.UpdateAsync(test);
            var updatedTestDto = _mapper.Map<UrineDipsticksTestDto>(updatedTest);
            return Ok(updatedTestDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}
