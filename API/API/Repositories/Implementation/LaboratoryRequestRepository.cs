using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Model;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class LaboratoryRequestRepository : ILaboratoryRequestRepository
{
    private readonly ApiContext _context;

    public LaboratoryRequestRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LaboratoryRequest>> GetAllRequests()
    {
        return await _context.LaboratoryRequests
            .Include(l => l.Parasitology)
            //.Include(l => l.Urinalysis)
            .Include(l => l.Hematology)
            .Include(l => l.Biochemistry)
            .Include(l => l.Bacteriology)
            .Include(l => l.Serology)
            .Include(l => l.CardiacMarker)
            .Include(l => l.CancerMarker)
            .Include(l => l.Electrolyte)
            .Include(l => l.OtherLab)
            .Include(l => l.Hormone)
            .Include(l => l.Coagulation)
            .ToListAsync();
    }

    public async Task<LaboratoryRequest> GetRequestById(int id)
    {
        return await _context.LaboratoryRequests
            .Include(l => l.Parasitology)
            //.Include(l => l.Urinalysis)
            .Include(l => l.Hematology)
            .Include(l => l.Biochemistry)
            .Include(l => l.Bacteriology)
            .Include(l => l.Serology)
               .Include(l => l.CardiacMarker)
            .Include(l => l.CancerMarker)
            .Include(l => l.Electrolyte)
            .Include(l => l.OtherLab)
            .Include(l => l.Hormone)
            .Include(l => l.Coagulation)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<LaboratoryRequest> CreateRequest(LaboratoryRequest request)
    {
        _context.LaboratoryRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task<LaboratoryRequest> UpdateRequest(LaboratoryRequest request)
    {
        _context.Entry(request).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task DeleteRequest(int id)
    {
        var request = await _context.LaboratoryRequests.FindAsync(id);
        if (request != null)
        {
            _context.LaboratoryRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}
