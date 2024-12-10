// API/Controllers/PrescriptionItemsController.cs

using API.Model.DTO;
using API.Model;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Repositories.Implementation;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API.Controllers
{// Controllers/PrescriptionItemsController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionItemsController : ControllerBase
    {
        private readonly IPrescriptionItemRepository _prescriptionItemRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly StockDbContext _context;
        private readonly ILogger<PrescriptionItemsController> _logger;
        public PrescriptionItemsController(IPrescriptionRepository prescriptionRepository,
                                       IPrescriptionItemRepository prescriptionItemRepository,
                                       IStockRepository stockRepository,
                                       IMapper mapper, StockDbContext context,
                                        ILogger<PrescriptionItemsController> logger)
        {
            _prescriptionRepository = prescriptionRepository;
            _prescriptionItemRepository = prescriptionItemRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptionItems()
        {
            try
            {
                var prescriptionItems = await _prescriptionItemRepository.GetAll();
                var prescriptionItemDtos = _mapper.Map<IEnumerable<PrescriptionItemDto>>(prescriptionItems);
                return Ok(prescriptionItemDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{prescriptionId}")]
        public async Task<IActionResult> GetItemsByPrescriptionId(Guid prescriptionId)
        {
            var items = await _prescriptionItemRepository.GetItemsByPrescriptionId(prescriptionId);
            if (items == null || !items.Any())
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescriptionItem(PrescriptionItemDto prescriptionItemDto)
        {
            try
            {
                // Map to PrescriptionItem entity
                var prescriptionItem = _mapper.Map<PrescriptionItem>(prescriptionItemDto);

                // Register the prescription item
                await _prescriptionItemRepository.Add(prescriptionItem);
                await _prescriptionItemRepository.SaveChanges();

                // Retrieve stock details to check availability
                var stock = await _stockRepository.GetByIdAsync(prescriptionItemDto.StockId);

                // Determine if the prescription item is internal
                if (stock != null)
                {
                    prescriptionItem.IsInternal = prescriptionItemDto.Quantity <= stock.Quantity;
                }
                else
                {
                    prescriptionItem.IsInternal = false; // No stock available
                }

                // Update the prescription item with the isInternal value
                await _prescriptionItemRepository.Update(prescriptionItem);
                await _prescriptionItemRepository.SaveChanges();

                return CreatedAtAction(nameof(GetItemsByPrescriptionId), new { id = prescriptionItem.Id }, prescriptionItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionItemById(Guid id)
        {
            var prescriptionItem = await _prescriptionItemRepository.GetItemsByPrescriptionId(id);
            if (prescriptionItem == null)
            {
                return NotFound();
            }

            var prescriptionItemDto = _mapper.Map<PrescriptionItemDto>(prescriptionItem);
            return Ok(prescriptionItemDto);
        }


        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedPrescriptions()
        {
            try
            {
                var approvedPrescriptions = await _prescriptionItemRepository.GetApprovedPrescriptions();
                return Ok(approvedPrescriptions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescriptionItem(Guid id)
        {
            try
            {
                await _prescriptionItemRepository.DeletePrescriptionItem(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApprovePrescriptionItem(Guid id)
        {
            _logger.LogInformation($"ApprovePrescriptionItem called with id: {id}");

            try
            {
                var prescriptionItem = await _prescriptionItemRepository.GetById(id);
                if (prescriptionItem == null)
                {
                    _logger.LogWarning($"Prescription Item with id {id} not found.");
                    return NotFound();
                }

                if (prescriptionItem.IsApproved)
                {
                    _logger.LogWarning($"Prescription Item with id {id} is already approved.");
                    return BadRequest($"Prescription Item with id {id} is already approved.");
                }

                var stockItem = await _stockRepository.GetByIdAsync(prescriptionItem.StockId);
                if (stockItem == null || prescriptionItem.Quantity > stockItem.Quantity)
                {
                    GenerateExternalPrescription(prescriptionItem.Quantity);
                    _logger.LogWarning($"Prescription Item with id {id} cannot be fully approved due to insufficient stock.");
                    return BadRequest($"Prescription Item with id {id} cannot be fully approved due to insufficient stock.");
                }

                prescriptionItem.IsApproved = true;
                prescriptionItem.ApprovedQuantity = prescriptionItem.Quantity;
                stockItem.Quantity -= prescriptionItem.Quantity;

                await _stockRepository.UpdateAsync(stockItem);
                await _prescriptionItemRepository.Update(prescriptionItem);
                await _prescriptionItemRepository.SaveChanges();

                _logger.LogInformation($"Successfully approved Prescription Item with id {id}.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Internal server error while processing Prescription Item with id {id}.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }


        private void GenerateExternalPrescription(int quantity)
        {
            // Implement logic to generate external prescription
            Console.WriteLine($"Generate external prescription for quantity {quantity}");
            Console.WriteLine("Dear Ethiopian Electric Power,");
            Console.WriteLine("We need additional stock due to insufficient quantity in our inventory.");
            Console.WriteLine($"Please take necessary action to provide {quantity} units.");
            Console.WriteLine("Sincerely,");
            Console.WriteLine("Your Clinic Name");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescriptionItem(Guid id, PrescriptionItemDto updatedPrescriptionItemDto)
        {
            try
            {
                var existingPrescriptionItem = await _prescriptionItemRepository.GetById(id);
                if (existingPrescriptionItem == null)
                    return NotFound();

                int oldQuantity = existingPrescriptionItem.Quantity;
                existingPrescriptionItem.Quantity = updatedPrescriptionItemDto.Quantity;

                await _prescriptionItemRepository.Update(existingPrescriptionItem);
                await _prescriptionItemRepository.SaveChanges();

                int quantityDifference = updatedPrescriptionItemDto.Quantity - oldQuantity;

                if (existingPrescriptionItem.IsApproved)
                {
                    var stockItem = await _stockRepository.GetByIdAsync(existingPrescriptionItem.StockId);
                    if (stockItem != null)
                    {
                        stockItem.Quantity -= quantityDifference;
                        await _stockRepository.UpdateAsync(stockItem);
                        await _stockRepository.SaveChanges();
                    }
                    else
                    {
                        return NotFound($"Stock item with ID {existingPrescriptionItem.StockId} not found.");
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }



}