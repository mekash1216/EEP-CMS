// API.Repositories.StockRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockDbContext _context;

        public StockRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stock.ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(Guid id)
        {
            return await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CreateAsync(Stock stock)
        {
            stock.TotalPrice = stock.Price * stock.Quantity;  // Calculate total price
            _context.Stock.Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stock stock)
        {
               // Calculate total price
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock != null)
            {
                _context.Stock.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
