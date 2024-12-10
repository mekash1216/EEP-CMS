using API.Data;
using API.Model;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{

    public class AccidentReportRepository : IAccidentReportRepository
    {
        private readonly ApiContext _context;

        public AccidentReportRepository(ApiContext context)
        {
            _context = context;
        }
   
        public async Task<AccidentReport> GetByIdAsync(int id)
        {
            return await _context.AccidentReports
                .Include(ar => ar.Patient)
                .Include(ar => ar.Witnesses)
                .FirstOrDefaultAsync(ar => ar.Id == id);
        }

        public async Task<IEnumerable<AccidentReport>> GetAllAsync()
        {
            return await _context.AccidentReports
                .Include(ar => ar.Patient) 
                .Include(ar => ar.Witnesses)  
                .ToListAsync();
        }


        public async Task<AccidentReport> AddAsync(AccidentReport accidentReport)
        {
            _context.AccidentReports.Add(accidentReport);
            await _context.SaveChangesAsync();
            return accidentReport;
        }

        public async Task<AccidentReport> UpdateAsync(AccidentReport accidentReport)
        {
            _context.AccidentReports.Update(accidentReport);
            await _context.SaveChangesAsync();
            return accidentReport;
        }

        public async Task DeleteAsync(int id)
        {
            var accidentReport = await _context.AccidentReports.FindAsync(id);
            if (accidentReport != null)
            {
                _context.AccidentReports.Remove(accidentReport);
                await _context.SaveChangesAsync();
            }
        }
    }

}
