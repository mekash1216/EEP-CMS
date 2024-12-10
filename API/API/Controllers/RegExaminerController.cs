using API.Model.DTO;
using API.Model;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class RegExaminerController : ControllerBase
        {
            private readonly IRegExaminerRepository _repository;
            private readonly IMapper _mapper;

            public RegExaminerController(IRegExaminerRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<RegExaminerDto>>> GetExaminers()
            {
                var examiners = await _repository.GetRegExaminersAsync();
                var examinersDTO = _mapper.Map<IEnumerable<RegExaminerDto>>(examiners);
                return Ok(examinersDTO);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<RegExaminerDto>> GetExaminerById(int id)
            {
                var examiner = await _repository.GetRegExaminerByIdAsync(id);
                if (examiner == null)
                {
                    return NotFound();
                }
                var examinerDTO = _mapper.Map<RegExaminerDto>(examiner);
                return Ok(examinerDTO);
            }

            [HttpPost]
            public async Task<ActionResult<RegExaminerDto>> AddExaminer(RegExaminerDto examinerDTO)
            {
                var examiner = _mapper.Map<RegExaminer>(examinerDTO);
                await _repository.AddRegExaminerAsync(examiner);
                return CreatedAtAction(nameof(GetExaminerById), new { id = examiner.Id }, _mapper.Map<RegExaminerDto>(examiner));
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateExaminer(int id, RegExaminerDto examinerDTO)
            {
                if (id != examinerDTO.Id)
                {
                    return BadRequest();
                }
                var examiner = _mapper.Map<RegExaminer>(examinerDTO);
                await _repository.UpdateRegExaminerAsync(examiner);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteExaminer(int id)
            {
                var examiner = await _repository.GetRegExaminerByIdAsync(id);
                if (examiner == null)
                {
                    return NotFound();
                }
                await _repository.DeleteRegExaminerAsync(id);
                return NoContent();
            }
        }
   }

