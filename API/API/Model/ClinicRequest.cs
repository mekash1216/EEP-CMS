using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class ClinicRequest
    {
        [Key]
        public int Id { get; set; }

        public string EmployeeId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientDepartment { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Workplace { get; set; }
        public bool IsManager { get; set; }
        public string Position { get; set; }
        public DateTime Birthdate { get; set; }
        public int age { get; set; }
        public DateTime RequestDate { get; set; }
        public string status { get; set; }

    }
}
