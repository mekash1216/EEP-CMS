// Prescription.cs
using API.Model;
using System.ComponentModel.DataAnnotations.Schema;

public class Prescription
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int Weight { get; set; }
    public bool IsInpatient { get; set; }
    public bool IsOutpatient { get; set; }
    public bool IsEmergency { get; set; }
    public string Diagnosis { get; set; }
    public bool Approved { get; set; }

    public int PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public Patient Patient { get; set; }

    //public ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
    public List<PrescriptionItem> PrescriptionItems { get; set; }
  
}
