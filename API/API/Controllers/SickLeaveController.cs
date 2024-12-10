using API.Model.DTO;
using API.Model;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SickLeaveController : ControllerBase
    {
        private readonly ISickLeaveRepository _repository;

        public SickLeaveController(ISickLeaveRepository repository)
        {
            _repository = repository;
        }

        // GET: api/sickleave
        [HttpGet]
        public async Task<ActionResult<List<SickLeave>>> GetSickLeaves()
        {
            var sickLeaves = await _repository.GetAllAsync();
            return Ok(sickLeaves);
        }

        // GET: api/sickleave/expired
        [HttpGet("expired")]
        public async Task<ActionResult<List<SickLeave>>> GetExpiredSickLeaves()
        {
            var expiredSickLeaves = await _repository.GetExpiredSickLeavesAsync();
            return Ok(expiredSickLeaves);
        }

        // POST: api/sickleave
        [HttpPost]
        public async Task<ActionResult> AddSickLeave(SickLeaveDto sickLeaveDto)
        {
            var sickLeave = new SickLeave
            {
               // EmployeeName = sickLeaveDto.EmployeeName,
                StartDate = sickLeaveDto.StartDate,
                EndDate = sickLeaveDto.EndDate,
                Reason = sickLeaveDto.Reason,
                PatientId = sickLeaveDto.PatientId
            };

            await _repository.AddAsync(sickLeave);
            return Ok();
        }
        // PUT: api/sickleave/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSickLeave(int id, SickLeaveUpdateDto sickLeaveDto)
        {
            var existingSickLeave = await _repository.GetByIdAsync(id);

            if (existingSickLeave == null)
                return NotFound();

            // Update properties
            existingSickLeave.StartDate = sickLeaveDto.StartDate;
            existingSickLeave.EndDate = sickLeaveDto.EndDate;
            existingSickLeave.Reason = sickLeaveDto.Reason;
          

            await _repository.UpdateAsync(existingSickLeave);
            return NoContent();
        }


        // DELETE: api/sickleave/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSickLeave(int id)
        {
            var sickLeave = await _repository.GetByIdAsync(id);

            if (sickLeave == null)
                return NotFound();

            await _repository.DeleteAsync(sickLeave);
            return Ok();
        }
    }
}