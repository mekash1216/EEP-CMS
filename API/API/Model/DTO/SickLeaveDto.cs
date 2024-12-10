namespace API.Model.DTO
{
    public class SickLeaveDto
    {
       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public int PatientId { get; set; }
    }

    public class SickLeaveUpdateDto
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
      
    }
}
