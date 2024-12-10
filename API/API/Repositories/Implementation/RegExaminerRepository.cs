using API.Data;
using API.Model;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Repositories.Implementation
{
    public class RegExaminerRepository : IRegExaminerRepository
    {
        private readonly ApiContext _context;

        public RegExaminerRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegExaminer>> GetRegExaminersAsync()
        {
            return await _context.RegExaminers.ToListAsync();
        }

        public async Task<RegExaminer> GetRegExaminerByIdAsync(int id)
        {
            return await _context.RegExaminers.FindAsync(id);
        }

        public async Task AddRegExaminerAsync(RegExaminer examiner)
        {
            _context.RegExaminers.Add(examiner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRegExaminerAsync(RegExaminer examiner)
        {
            _context.Entry(examiner).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRegExaminerAsync(int id)
        {
            var examiner = await _context.RegExaminers.FindAsync(id);
            if (examiner != null)
            {
                _context.RegExaminers.Remove(examiner);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RegExaminerExistsAsync(int id)
        {
            return await _context.RegExaminers.AnyAsync(e => e.Id == id);
        }
    }
}
