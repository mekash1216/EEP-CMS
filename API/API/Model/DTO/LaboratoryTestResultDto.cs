namespace API.Model.DTO
{
    public class LaboratoryTestResultDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Test { get; set; }
        public string Result { get; set; }
        public string ReferenceRange { get; set; }
        public string Units { get; set; }
        public string Gender { get; set; }
        public string Severity { get; set; }
        public int age { get; set; }
        public bool? IsPregnant { get; set; }
        public int PatientId { get; set; }
        public string? Phase { get; set; }
        public DateTime? TestDate { get; set; }
        public ICollection<LaboratorySubTestResultDto> SubTests { get; set; }
    }

    public class LaboratoryResultDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Test { get; set; }
        public string Result { get; set; }
        public string Gender { get; set; }
        public int age { get; set; }
        public bool? IsPregnant { get; set; }
        public string? Phase { get; set; }
        public int PatientId { get; set; }
        public DateTime? TestDate { get; set; }
        public ICollection<LaboratorySubTestResultDto> SubTests { get; set; }
    }

    public class LaboratorySubTestResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Result { get; set; }
        public string Gender { get; set; }
        public string? ReferenceRange { get; set; }
        public string? Units { get; set; }
        public string? Severity { get; set; }
        public DateTime? TestDate { get; set; }
    }
}
