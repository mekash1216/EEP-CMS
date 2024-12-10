namespace API.Model.DTO
{
    public class ReportDTO
    {
        public string PatientCardNo { get; set; }
        public DateTime? Date { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Diagnosis { get; set; }
     
    }
}
