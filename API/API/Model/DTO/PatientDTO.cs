namespace API.Model.DTO
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string? cardNo { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public DateTime assigneddate { get; set; }
        public string PatientDepartment { get; set; }
        public string Email { get; set; }
        public string Workplace { get; set; }
        public string Position { get; set; }
        public int? age { get; set; }
        public string? phoneNumber { get; set; }
  

     
    }
 
}
