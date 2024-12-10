namespace API.Model
{
    public class SickLeave
    {

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public bool IsExpired => EndDate < DateTime.Now;

        public int PatientId { get; set; }  
        public Patient Patient { get; set; } 
    }
}
