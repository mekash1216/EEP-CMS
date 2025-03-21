using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Examiner
    {
        public int Id { get; set; }
        public DateTime AssignedDate { get; set; }
        // public string ExaminerName { get; set; }
        [ForeignKey(nameof(Patient))]
        public int? PatientId { get; set; }
        public virtual Patient? Patient { get; set; }

      [ForeignKey(nameof(Assignment))]
       public int? AssignmentId { get; set; }
       public virtual Assignment Assignment { get; set; }

    }
}
