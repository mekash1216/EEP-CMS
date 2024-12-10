using API.Data;
using API.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApiContext _context;

        public DoctorRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

      
        public async Task AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor); // Don't set doctor.Id here
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
