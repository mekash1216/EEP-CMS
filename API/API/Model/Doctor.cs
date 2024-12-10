using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Specialty { get; set; }

        // Navigation property
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

        [NotMapped]
        public ICollection<Examiner> Examiner { get; set; }
        public ICollection<Examiner> Examiners { get; set; } = new List<Examiner>();
    }
}
