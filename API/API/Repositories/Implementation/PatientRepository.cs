using API.Data;
using API.Model;
using API.Model.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public PatientRepository(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }


        public async Task AddAsync(PatientDTO assignmentDTO)
        {
            var assignment = _mapper.Map<Patient>(assignmentDTO);
            await _context.Patients.AddAsync(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientDataAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                //patient.Weight = weight;
                //patient.Pressure = pressure;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Patient>> GetPatientsByDayAsync(DateTime date)
        {
            return await _context.Patients
                .Where(p => p.assigneddate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetPatientsByMonthAsync(int year, int month)
        {
            return await _context.Patients
                .Where(p => p.assigneddate.Year == year && p.assigneddate.Month == month)
                .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetPatientsByYearAsync(int year)
        {
            return await _context.Patients
                .Where(p => p.assigneddate.Year == year)
                .ToListAsync();
        }
    }
}
