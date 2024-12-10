using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Model;
using API.Data;
using API.Repositories.Interface;
using API.Model.DTO;
using System.Drawing;

namespace API.Repositories.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApiContext _context;

        public AppointmentRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await _context.Appointments
       .Include(a => a.Examiner)  // Include related Examiner data
       .ThenInclude(e => e.Patient)  // Include related Patient data
       //.ThenInclude(e => e.RegExaminer)  
       .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
               .Include(a => a.Examiner)
                //.Include(a => a.RegExaminer)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<AppointmentDto> GetAppointmentsByPatient(int patientId)
        {
            return _context.Appointments
                .Include(a => a.Examiner)
                .ThenInclude(e => e.Patient)
                .Where(a => a.Examiner.PatientId == patientId)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    AppointmentDateTime = a.AppointmentDateTime,
                    ExaminerId = a.ExaminerId,
                    PatientCardNumber = a.Examiner.Patient.cardNo,
                    PatientFirstName = a.Examiner.Patient.firstName,
                    PatientLastName = a.Examiner.Patient.lastName,
                    Reason= a.Reason,
                })
                .ToList();
        }

    }
}
