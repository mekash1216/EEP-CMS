using API.Data;
using API.Model;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class UrineDipsticksTestRepository : IUrineDipsticksTestRepository
    {
        private readonly ApiContext _context;

        public UrineDipsticksTestRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UrineDipsticksTest>> GetAllAsync()
        {
            return await _context.UrineDipsticksTests.ToListAsync();
        }

        public async Task<UrineDipsticksTest> GetByIdAsync(int id)
        {
            return await _context.UrineDipsticksTests.FindAsync(id);
        }

        public async Task<UrineDipsticksTest> AddAsync(UrineDipsticksTest test)
        {
            _context.UrineDipsticksTests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<UrineDipsticksTest> UpdateAsync(UrineDipsticksTest test)
        {
            _context.Entry(test).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var test = await _context.UrineDipsticksTests.FindAsync(id);
            if (test == null)
            {
                return false;
            }

            _context.UrineDipsticksTests.Remove(test);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
