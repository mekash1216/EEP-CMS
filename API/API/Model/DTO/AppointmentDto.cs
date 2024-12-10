namespace API.Model.DTO
{
    // DTOs/AppointmentDto.cs
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Reason { get; set; }
        public int? ExaminerId { get; set; }
        //public int RegExaminerId { get; set; }
       public string? PatientCardNumber { get; set; }
        public string? PatientFirstName { get; set; }
        public string? PatientLastName { get; set; }
        //public string RegExaminerId { get; set; }
       
    }

    // DTOs/CreateAppointmentDto.cs
    public class CreateAppointmentDto
    {
        public DateTime AppointmentDateTime { get; set; } 
        public int ExaminerId { get; set; }
        public string? Reason { get; set; }
     
        //public int RegExaminerId { get; set; }
    }

}
