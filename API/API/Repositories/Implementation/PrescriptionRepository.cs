using API.Data;
using API.Model;
using API.Model.DTO;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Repositories.Implementation
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApiContext _context;

        public PrescriptionRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task Add(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
        }

        public async Task<IEnumerable<PrescriptionDto>> GetAll()
        {
            return await _context.Prescriptions
                .Include(p => p.PrescriptionItems)
                .Include(p => p.Patient) 
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id,
                    Date = p.Date,
                    IsInpatient = p.IsInpatient,
                    IsOutpatient = p.IsOutpatient,
                    IsEmergency = p.IsEmergency,
                    Diagnosis = p.Diagnosis,
                    CardNumber = p.Patient.cardNo,
                    NameOfPatient = $"{p.Patient.firstName} {p.Patient.lastName}",
                    Age = p.Patient.age,
                    PrescriptionItems = p.PrescriptionItems.Select(pi => new PrescriptionItemDto
                    {
                        Id = pi.Id,
                        PrescriptionId = pi.PrescriptionId,
                        StockId = pi.StockId,
                        Quantity = pi.Quantity,
                        IsInternal = pi.IsInternal,
                        IsApproved = pi.IsApproved,
                        
                    }).ToList()
                }).ToListAsync();
        }


        public async Task<Prescription> GetById(Guid id)
        {
            return await _context.Prescriptions
                                 .Include(p => p.PrescriptionItems)
                                 .ThenInclude(pi => pi.Stock)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Update(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
        }

        public void Remove(Prescription prescription)
        {
            _context.Prescriptions.Remove(prescription);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<PrescriptionDto>> GetPrescriptionsByPatientAsync(int patientId)
        {
            return await _context.Prescriptions
                .Where(p => p.PatientId == patientId)
                .Select(p => new PrescriptionDto
                {
                   // Id = p.Id,
                    Date = p.Date,
                    IsInpatient = p.IsInpatient,
                    IsOutpatient = p.IsOutpatient,
                    IsEmergency = p.IsEmergency,
                    Diagnosis = p.Diagnosis,
                   // Approved = p.Approved,
                   // PatientId = p.PatientId,
                    CardNumber = p.Patient.cardNo,
                    NameOfPatient = $"{p.Patient.firstName} {p.Patient.lastName}",
                    Age = p.Patient.age,
                    //Sex = p.Patient.,
                    PrescriptionItems = p.PrescriptionItems.Select(pi => new PrescriptionItemDto
                    {
                       // Id = pi.Id,
                        PrescriptionId = pi.PrescriptionId,
                        StockId = pi.StockId,
                        Quantity = pi.Quantity,
                        //IsApproved = pi.IsApproved,
                        IsInternal = pi.IsInternal,
                       // ApprovedQuantity = pi.ApprovedQuantity,
                        StockAvailable = pi.StockAvailable
                    }).ToList()
                }).ToListAsync();
        }
 
        public async Task<List<ReportDTO>> GetPrescriptionReportsAsync()
        {
            var result = await (from examiner in _context.Examiners
                                join patient in _context.Patients on examiner.PatientId equals patient.Id
                                join prescription in _context.Prescriptions on patient.Id equals prescription.PatientId
                                select new ReportDTO
                                {
                                    PatientCardNo = patient.cardNo,
                                   // PatientName = patient.FirstName + " " + patient.LastName,
                                    Age = patient.age,
                                    Gender = patient.gender,
                                    Diagnosis = prescription.Diagnosis,
                                    Date = examiner.AssignedDate,
                                }).ToListAsync();

            return result;
        }



    }
}
