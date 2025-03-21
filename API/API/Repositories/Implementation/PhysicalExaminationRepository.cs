using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
   public class PhysicalExaminationRepository : IPhysicalExaminationRepository
{
    private readonly ApiContext _context;

    public PhysicalExaminationRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PhysicalExamination>> GetAllAsync()
    {
        return await _context.PhysicalExaminations
           .Include(a => a.Patient)
        .ToListAsync();

       
    }

    public async Task<PhysicalExamination> GetByIdAsync(int id)
    {
        return await _context.PhysicalExaminations.FindAsync(id);
    }

    public async Task<PhysicalExamination> AddAsync(PhysicalExamination physicalExamination)
    {
        _context.PhysicalExaminations.Add(physicalExamination);
        await _context.SaveChangesAsync();
        return physicalExamination;
    }

        public async Task UpdateAsync(PhysicalExamination physicalExamination)
        {
            _context.Entry(physicalExamination).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
    {
        var physicalExamination = await _context.PhysicalExaminations.FindAsync(id);
        if (physicalExamination != null)
        {
            _context.PhysicalExaminations.Remove(physicalExamination);
            await _context.SaveChangesAsync();
        }
    }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PhysicalExaminations.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<PhysicalExamination>> GetByPatientIdAsync(int patientId)
        {
            return await _context.PhysicalExaminations
                                 .Where(pe => pe.PatientId == patientId)
                                 .ToListAsync();
        }

    }

}
