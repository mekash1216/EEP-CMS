namespace API.Model.DTO
{
    public class RegisterPatientDTO
    {
      
        public string?   cardNo { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int? age { get; set; }
        public string?   phoneNumber { get; set; }
        //public int DoctorId { get; set; }
        //public float? Weight { get; set; }
        //public float? Pressure { get; set; }
    }
}
