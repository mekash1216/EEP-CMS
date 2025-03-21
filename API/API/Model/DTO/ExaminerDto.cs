
namespace API.Model.DTO
{
    public class ExaminerDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AssignmentId { get; set; }
        // public string ExaminerName { get; set; }
        public DateTime AssignedDate { get; set; }
    }
    public class ExaminerDetailDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? PatientCardNumber { get; set; }
        public int PatientAge { get; set; }
        public string? PatientFirstName { get; set; }
        public string? PatientLastName { get; set; }
        public float Weight { get; set; }
        public float Pressure { get; set; }
        // public string ExaminerName { get; set; }
        public DateTime AssignedDate { get; set; }
    }

    public class UpdateExaminerDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AssignmentId { get; set; }
        // public string ExaminerName { get; set; }
        public DateTime AssignedDate { get; set; }
    }

}
