namespace API.Model
{
    public class PhysicalExamination
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
         public Patient Patient { get; set; } 
        public string GeneralAppearance { get; set; }
        public string VitalSigns { get; set; }
        public string HeadAndNeck { get; set; }
        public string Cardiovascular { get; set; }
        public string Respiratory { get; set; }
        public string Abdomen { get; set; }
        public DateTime ExaminationDate { get; set; }
    }
}
