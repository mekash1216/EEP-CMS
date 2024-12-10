using API.Model;
using API.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task CreateAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int id);
        //Task SaveAsync();  
    
        IEnumerable<AppointmentDto> GetAppointmentsByPatient(int patientId);
    }
}
