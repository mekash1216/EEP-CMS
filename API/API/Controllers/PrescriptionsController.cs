using API.Model.DTO;
using API.Model;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using API.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IPrescriptionItemRepository _prescriptionItemRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public PrescriptionsController(IPrescriptionRepository prescriptionRepository,
                                       IPrescriptionItemRepository prescriptionItemRepository,
                                       IStockRepository stockRepository,
                                       IMapper mapper)
        {
            _prescriptionRepository = prescriptionRepository;
            _prescriptionItemRepository = prescriptionItemRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prescriptions = await _prescriptionRepository.GetAll();
            return Ok(prescriptions);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionById(Guid id)
        {
            var prescription = await _prescriptionRepository.GetById(id);
            if (prescription == null)
            {
                return NotFound();
            }

            var prescriptionDto = _mapper.Map<PrescriptionDto>(prescription);
            return Ok(prescriptionDto);
        }



        [HttpPost]
        public async Task<IActionResult> CreatePrescription(PrescriptionCreateDto prescriptionDto)
        {
            try
            {
                // Map DTO to Prescription
                var prescription = _mapper.Map<Prescription>(prescriptionDto);

                // Check and set isInternal for each prescription item
                foreach (var item in prescription.PrescriptionItems)
                {
                    var stock = await _stockRepository.GetByIdAsync(item.StockId);
                    if (stock != null)
                    {
                        item.IsInternal = item.Quantity <= stock.Quantity;
                    }
                }

                await _prescriptionRepository.Add(prescription);
                await _prescriptionRepository.SaveChanges();

                return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.Id }, prescription);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(Guid id, PrescriptionUpdateDto prescriptionDto)
        {
            if (id != prescriptionDto.Id)
            {
                return BadRequest("Prescription ID mismatch");
            }

            try
            {
                var prescription = await _prescriptionRepository.GetById(id);
                if (prescription == null)
                {
                    return NotFound();
                }

                _mapper.Map(prescriptionDto, prescription);
                _prescriptionRepository.Update(prescription);
                await _prescriptionRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(Guid id)
        {
            try
            {
                var prescription = await _prescriptionRepository.GetById(id);
                if (prescription == null)
                {
                    return NotFound();
                }

                _prescriptionRepository.Remove(prescription);
                await _prescriptionRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("ByPatient/{patientId}")]
        public async Task<ActionResult<IEnumerable<PrescriptionDto>>> GetPrescriptionsByPatient(int patientId)
        {
            var prescriptions = await _prescriptionRepository.GetPrescriptionsByPatientAsync(patientId);
            if (prescriptions == null || !prescriptions.Any())
            {
                return NotFound();
            }
            return Ok(prescriptions);
        }

        [HttpGet("prescription-report")]
        public async Task<IActionResult> GetPrescriptionReport()
        {
            var report = await _prescriptionRepository.GetPrescriptionReportsAsync();
            return Ok(report);
        }

    }
}