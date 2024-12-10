using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public string? cardNo { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public DateTime assigneddate { get; set; }
        public string PatientDepartment { get; set; }
        public string Email { get; set; }
        public string Workplace { get; set; }
        public string Position { get; set; }
        public int? age { get; set; }
        public string? phoneNumber { get; set; }
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        [NotMapped]
        public Examiner Examiner { get; set; }
        public ICollection<Examiner> Examiners { get; set; } = new List<Examiner>();
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<LaboratoryRequest> LaboratoryRequests { get; set; }

        public ICollection<SickLeave> SickLeaves { get; set; }

        public int Age => CalculateAge();

        private int CalculateAge()
        {
            var today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
