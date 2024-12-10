using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.DTO
{
    public class PrescriptionDto
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime Date { get; set; }
        public string NameOfPatient { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }
        public int Weight { get; set; }
        public bool IsInpatient { get; set; }
        public bool IsOutpatient { get; set; }
        public bool IsEmergency { get; set; }
        public string Diagnosis { get; set; }
        // public ICollection<PrescriptionItemDto> PrescriptionItems { get; set; }
        public List<PrescriptionItemDto>? PrescriptionItems { get; set; }

    }

    public class PrescriptionCreateDto
    {
     public DateTime Date { get; set; }
    public int Weight { get; set; }
    public bool IsInpatient { get; set; }
    public bool IsOutpatient { get; set; }
    public bool IsEmergency { get; set; }
    public string Diagnosis { get; set; }
   // public bool Approved { get; set; }
    public int PatientId { get; set; }
        // public ICollection<PrescriptionItemDto> PrescriptionItems { get; set; }
        public List<PrescriptionItemCreateDto>? PrescriptionItems { get; set; }
    }

    public class PrescriptionUpdateDto
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime Date { get; set; }
        public string NameOfPatient { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public int Weight { get; set; }
        public bool IsInpatient { get; set; }
        public bool IsOutpatient { get; set; }
        public bool IsEmergency { get; set; }
        public string Diagnosis { get; set; }

    }
}
