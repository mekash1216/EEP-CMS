// API.Controllers.StocksController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Model.DTO;
using API.Models;
using API.Repositories;
using API.Repositories.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly IPrescriptionItemRepository _prescriptionItemRepository; // Add this line
        private readonly IMapper _mapper;

        public StocksController(IStockRepository stockRepository, IPrescriptionItemRepository prescriptionItemRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _prescriptionItemRepository = prescriptionItemRepository; // Assign here
            _mapper = mapper;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockReadDto>>> GetStocks()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<StockReadDto>>(stocks));
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockReadDto>> GetStock(Guid id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StockReadDto>(stock));
        }

        // POST: api/Stocks
        [HttpPost]
        public async Task<ActionResult<StockReadDto>> PostStock(StockCreateDto stockCreateDto)
        {
            var stock = _mapper.Map<Stock>(stockCreateDto);
            await _stockRepository.CreateAsync(stock);

            var stockReadDto = _mapper.Map<StockReadDto>(stock);
            return CreatedAtAction(nameof(GetStock), new { id = stockReadDto.Id }, stockReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(Guid id, StockUpdateDto stockUpdateDto)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            // Update the stock details
            _mapper.Map(stockUpdateDto, stock);
            await _stockRepository.UpdateAsync(stock);
            await _stockRepository.SaveChanges(); // Save changes first

            // Get all prescription items related to the stock
            var prescriptionItems = await _prescriptionItemRepository.GetAll();
            foreach (var item in prescriptionItems)
            {
                // Update IsInternal based on the updated stock quantity
                item.IsInternal = item.Quantity <= stock.Quantity;
                await _prescriptionItemRepository.Update(item);
            }

            await _prescriptionItemRepository.SaveChanges(); // Save changes for prescription items

            return NoContent();
        }



        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(Guid id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            await _stockRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
