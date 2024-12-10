using API.Data;
using API.Model;
using API.Model.DTO;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class PhysicalAssessmentRepository: IPhysicalAssessmentRepository
    {
        private readonly ApiContext _context;

        public PhysicalAssessmentRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PhysicalAssessment>> GetAllAsync()
        {
            return await _context.PhysicalAssessments.ToListAsync();
        }

        public async Task<PhysicalAssessment> GetByIdAsync(int id)
        {
            return await _context.PhysicalAssessments.FindAsync(id);
        }

        public async Task<PhysicalAssessment> AddAsync(PhysicalAssessment physicalExamination)
        {
            _context.PhysicalAssessments.Add(physicalExamination);
            await _context.SaveChangesAsync();
            return physicalExamination;
        }

        public async Task UpdateAsync(PhysicalAssessment physicalExamination)
        {
            _context.Entry(physicalExamination).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
        {
            var physicalExamination = await _context.PhysicalAssessments.FindAsync(id);
            if (physicalExamination != null)
            {
                _context.PhysicalAssessments.Remove(physicalExamination);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PhysicalExaminations.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<PhysicalAssessment>> GetByPatientIdAsync(int patientId)
        {
            return await _context.PhysicalAssessments
                                 .Where(pe => pe.PatientId == patientId)
                                 .ToListAsync();
        }

    }

}
