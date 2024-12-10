using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Model.DTO;
using API.Repositories;
using System.Threading.Tasks;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Microsoft.Data.SqlClient;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ApiContext _context;


        public AppointmentController(IAppointmentRepository appointmentRepository, ApiContext context)
        {
            _appointmentRepository = appointmentRepository;
            _context = context;
        }

        [HttpGet]
      
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _appointmentRepository.GetAppointmentsAsync();
            var appointmentDtos = appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                AppointmentDateTime = a.AppointmentDateTime,
                ExaminerId = a.ExaminerId.GetValueOrDefault(),
                PatientCardNumber = a.Examiner?.Patient?.cardNo,
                PatientFirstName = a.Examiner?.Patient?.firstName,
                PatientLastName = a.Examiner?.Patient?.lastName,
                Reason =a.Reason,

            });

            return Ok(appointmentDtos);
        }
        [HttpGet("active")]
        public IActionResult GetActiveAppointments()
        {
            try
            {
                var activeAppointments = _context.Appointments
                    .Where(a => a.AppointmentDateTime >= DateTime.Now)
                    .ToList();

                return Ok(activeAppointments);
            }
            catch (SqlException ex)
            {
                // Log the error
                return StatusCode(500, "Database connection error: " + ex.Message);
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();

            var appointmentDto = new AppointmentDto
            {
                Id = appointment.Id,
                AppointmentDateTime = appointment.AppointmentDateTime,
                ExaminerId = appointment.ExaminerId.GetValueOrDefault(),
                //RegExaminerId = appointment.RegExaminerId.GetValueOrDefault()
            };

            return Ok(appointmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                AppointmentDateTime = dto.AppointmentDateTime,
                ExaminerId = dto.ExaminerId,
                Reason=dto.Reason,
                //RegExaminerId = dto.RegExaminerId
            };

            await _appointmentRepository.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, CreateAppointmentDto dto)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (existingAppointment == null) return NotFound();

            existingAppointment.AppointmentDateTime = dto.AppointmentDateTime;
            existingAppointment.ExaminerId = dto.ExaminerId;
            existingAppointment.Reason = dto.Reason;
            //existingAppointment.RegExaminerId = dto.RegExaminerId;

            await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentRepository.DeleteAppointmentAsync(id);
            return NoContent();
        }

        [HttpGet("Appointments/ByPatient/{patientId}")]
        public IActionResult GetAppointmentsByPatient(int patientId)
        {
            var appointments = _appointmentRepository.GetAppointmentsByPatient(patientId);
            return Ok(appointments);
        }

    }
}
