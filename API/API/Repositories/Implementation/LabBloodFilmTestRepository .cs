using API.Model;
using API.Repositories;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LabBloodFilmTestRepository : ILabBloodFilmTestRepository
    {
        private readonly ApiContext _context;

        public LabBloodFilmTestRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<LabBloodFilmTest> GetBloodFilmTestByIdAsync(int id)
        {
            return await _context.BloodFilmTests.FindAsync(id);
        }

        public async Task<IEnumerable<LabBloodFilmTest>> GetAllBloodFilmTestsAsync()
        {
            return await _context.BloodFilmTests.ToListAsync();
        }

        public async Task<LabBloodFilmTest> CreateBloodFilmTestAsync(LabBloodFilmTest bloodFilmTest)
        {
            _context.BloodFilmTests.Add(bloodFilmTest);
            await _context.SaveChangesAsync();
            return bloodFilmTest;
        }

        public async Task<LabBloodFilmTest> UpdateBloodFilmTestAsync(LabBloodFilmTest bloodFilmTest)
        {
            _context.BloodFilmTests.Update(bloodFilmTest);
            await _context.SaveChangesAsync();
            return bloodFilmTest;
        }

        public async Task DeleteBloodFilmTestAsync(int id)
        {
            var bloodFilmTest = await _context.BloodFilmTests.FindAsync(id);
            if (bloodFilmTest != null)
            {
                _context.BloodFilmTests.Remove(bloodFilmTest);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<LabBloodFilmTest>> GetBloodFilmTestsByPatientIdAsync(int patientId)
        {
            return await _context.BloodFilmTests
                .Where(test => test.PatientId == patientId)
                .ToListAsync();
        }

    }
}
