// API.Repositories.IStockRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock> GetByIdAsync(Guid id);
        Task CreateAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(Guid id);
        Task SaveChanges();
    }
}
