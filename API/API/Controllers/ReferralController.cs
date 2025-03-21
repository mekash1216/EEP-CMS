
using API.Data;
using API.Model;
using API.Model.DTO;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferralController : ControllerBase
    {
        private readonly IReferralRepository _referralRepository;
        private readonly IMapper _mapper;
        private readonly ApiContext _context;
        private readonly ILogger<ReferralController> _logger;
        public ReferralController(IReferralRepository referralRepository, IMapper mapper, ApiContext context, ILogger<ReferralController> logger)
        {
            _referralRepository = referralRepository;
            _mapper = mapper;
            _context = context;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> GetReferrals()
        {
            try
            {
                var referrals = await _referralRepository.GetAllReferralsAsync();

                var referralDtos = referrals.Select(r => new
                {
                    r.Id,
                    r.ReferralDate,
                    r.InvestigationResult,
                    r.Reason,
                    r.Diagnosis,
                    r.Rxgiven,
                    r.Clinicalfinding,
                    r.To,
                    r.From,
                    Examiner = new
                    {
                        Id = r.Examiner.Id,
                        PatientName = $"{r.Examiner.Patient.firstName} {r.Examiner.Patient.lastName}",
                        // RegExaminerName = $"{r.Examiner.ExaminerName}"
                    }
                }).Distinct(); // Ensure uniqueness

                return Ok(referralDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving referrals.");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ReferralDto>> GetReferral(int id)
        {
            var referral = await _referralRepository.GetReferralByIdAsync(id);
            if (referral == null)
            {
                return NotFound();
            }
            var referralDTO = _mapper.Map<ReferralDto>(referral);
            return Ok(referralDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostReferral([FromBody] ReferralCreateDto referralDto)
        {
            var examiner = await _context.Examiners
                .Include(e => e.Patient)
                .SingleOrDefaultAsync(e => e.Id == referralDto.ExaminerId);

            if (examiner == null)
            {
                return NotFound();
            }

            var referral = new Referral
            {
                ReferralDate = referralDto.ReferralDate,
                InvestigationResult = referralDto.InvestigationResult,
                Reason = referralDto.Reason,
                Clinicalfinding=referralDto.Clinicalfinding,
                Rxgiven=referralDto.Rxgiven,
                Diagnosis=referralDto.Diagnosis,
                To=referralDto.To,
                From=referralDto.From,

                ExaminerId = referralDto.ExaminerId,
                Examiner = examiner
            };

            await _referralRepository.AddReferralAsync(referral);

            var response = new
            {
                referral.Id,
                referral.ReferralDate,
                referral.InvestigationResult,
                referral.Reason,
                referral.From,
                referral.Rxgiven,
                referral.Clinicalfinding,
                referral.Diagnosis,
                referral.To,
                

                Examiner = new
                {
                    examiner.Id,
                    examiner.AssignedDate,
                    Patient = new
                    {
                        examiner.Patient.Id,
                        examiner.Patient.firstName,
                        examiner.Patient.lastName
                    },

                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReferral(int id, ReferralCreateDto referralDTO)
        {
            if (id != referralDTO.Id)
            {
                return BadRequest();
            }

            var referral = _mapper.Map<Referral>(referralDTO);
            await _referralRepository.UpdateReferralAsync(referral);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferral(int id)
        {
            try
            {
                bool result = await _referralRepository.DeleteReferralAsync(id);
                if (!result)
                {
                    // Return a 404 Not Found response if the referral with the specified ID was not found
                    return NotFound(new { error = $"Referral with ID {id} not found." });
                }

                return NoContent(); // 204 No Content response indicating successful deletion
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error deleting referral with ID {id}: {ex.Message}");

                // Return a 500 Internal Server Error response with an error message
                return StatusCode(500, new { error = "An unexpected error occurred while deleting the referral." });
            }
        }

        [HttpGet("ByPatient/{patientId}")]
        public async Task<IActionResult> GetReferralsByPatient(int patientId)
        {
            try
            {
                var referrals = await _referralRepository.GetByPatientId(patientId);

                var referralDtos = referrals.Select(r => new
                {
                    r.Id,
                    r.ReferralDate,
                    r.InvestigationResult,
                    r.Reason,
                    r.Diagnosis,
                    r.Rxgiven,
                    r.Clinicalfinding,
                    r.To,
                    r.From,

                    Examiner = new
                    {
                        Id = r.Examiner.Id,
                        PatientName = $"{r.Examiner.Patient.firstName} {r.Examiner.Patient.lastName}",
                        // RegExaminerName = $"{r.Examiner.ExaminerName}"
                    }
                }).Distinct(); // Ensure uniqueness

                return Ok(referralDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving referrals by patient.");
                return StatusCode(500, "Internal server error");
            }
        }


    }

}
