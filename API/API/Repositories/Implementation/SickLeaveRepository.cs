using API.Data;
using API.Model;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Repositories.Implementation
{
    public class SickLeaveRepository : ISickLeaveRepository
    {
        private readonly ApiContext _context;

        public SickLeaveRepository(ApiContext context)
        {
            _context = context;
        }

        // Get all sick leaves
        public async Task<List<SickLeave>> GetAllAsync()
        {
            return await _context.SickLeaves
         .Include(s => s.Patient)  
         .ToListAsync();
        }

        // Get a specific sick leave by ID
        public async Task<SickLeave> GetByIdAsync(int id)
        {
            return await _context.SickLeaves
        .Include(s => s.Patient)
        .FirstOrDefaultAsync(s => s.Id == id);
        }

        // Get expired sick leaves where the end date is less than today
        public async Task<List<SickLeave>> GetExpiredSickLeavesAsync()
        {
            return await _context.SickLeaves
                .Where(s => s.EndDate < System.DateTime.Now)
                .ToListAsync();
        }

        // Add a new sick leave
        public async Task AddAsync(SickLeave sickLeave)
        {
            _context.SickLeaves.Add(sickLeave);
            await _context.SaveChangesAsync();
        }
        // Update an existing sick leave
        public async Task UpdateAsync(SickLeave sickLeave)
        {
            _context.SickLeaves.Update(sickLeave);
            await _context.SaveChangesAsync();
        }


        // Delete an existing sick leave
        public async Task DeleteAsync(SickLeave sickLeave)
        {
            _context.SickLeaves.Remove(sickLeave);
            await _context.SaveChangesAsync();
        }
    }
}