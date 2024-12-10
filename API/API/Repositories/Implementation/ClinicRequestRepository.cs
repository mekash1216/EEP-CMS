using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using API.Model;
using API.Repositories.Interface;
using API.Data;

public class ClinicRequestRepository : IClinicRequestRepository
{
    private readonly ApiContext _context;

    public ClinicRequestRepository(ApiContext context)
    {
        _context = context;
    }

    public async Task<ClinicRequest> GetByIdAsync(int id)
    {
        return await _context.ClinicRequests.FindAsync(id);
    }

    public async Task<IEnumerable<ClinicRequest>> GetAllAsync()
    {
        return await _context.ClinicRequests.ToListAsync();
    }

    public async Task AddAsync(ClinicRequest request)
    {
        _context.ClinicRequests.Add(request);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ClinicRequest request)
    {
        // Fetch the existing entity
        var existingRequest = await _context.ClinicRequests.FindAsync(request.Id);
        if (existingRequest == null)
        {
            throw new InvalidOperationException("Entity not found");
        }

        // Update the existing entity's properties
        existingRequest.EmployeeId = request.EmployeeId;
        existingRequest.PatientFirstName = request.PatientFirstName;
        existingRequest.PatientLastName = request.PatientLastName;
        existingRequest.PatientDepartment = request.PatientDepartment;
        existingRequest.Gender = request.Gender;
        existingRequest.PhoneNumber = request.PhoneNumber;
        existingRequest.Birthdate = request.Birthdate;
        existingRequest.RequestDate = request.RequestDate;
        existingRequest.status = request.status;
        existingRequest.age = request.age;

        // Save changes
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var request = await GetByIdAsync(id);
        if (request != null)
        {
            _context.ClinicRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }


    // New methods for Patient operations
    public async Task<Patient> GetPatientByCardNoAsync(string cardNo)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.cardNo == cardNo);
    }

    public async Task AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePatientAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }
}
