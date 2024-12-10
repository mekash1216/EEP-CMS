using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        //public DateTime Date { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Reason { get; set; }

        [ForeignKey(nameof(Examiner))]
        public int? ExaminerId { get; set; }
        public virtual Examiner? Examiner { get; set; }
        public string? PatientCardNumber { get; set; }
        public string? PatientFirstName { get; set; }
        public string? PatientLastName { get; set; }
        public string? RegExaminerFirstName { get; set; }
        

    }
}
