using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class LaboratoryTestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Category { get; set; }
        public string Test { get; set; }
        public string Result { get; set; }
        public string? ReferenceRange { get; set; }
        public string? Units { get; set; }
        public string Gender { get; set; }
        public string? Severity { get; set; }
        public int PatientId { get; set; }
        public int age { get; set; }
        public bool? IsPregnant { get; set; }  
        public string? Phase { get; set; }
        public DateTime? TestDate { get; set; }
        public ICollection<LaboratorySubTestResult> SubTests { get; set; } = new List<LaboratorySubTestResult>();
    }

    public class LaboratorySubTestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Result { get; set; }
        public string Gender { get; set; }
        public string? ReferenceRange { get; set; }
        public string? Units { get; set; }
        public string? Severity { get; set; }
        public DateTime? TestDate { get; set; }
        public int LaboratoryTestResultId { get; set; }
        public LaboratoryTestResult LaboratoryTestResult { get; set; }
    }
}
