namespace API.Model.DTO
{
    public class AssignmentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        //public int DoctorId { get; set; }
        public float Weight { get; set; }
        public float SystolicPressure { get; set; }
        public float DiastolicPressure { get; set; }
        public float RespiratoryRate { get; set; }
        public float PulseRate { get; set; }
        public float Temperature { get; set; }
        public float SpO2 { get; set; }
        public string Triage { get; set; }
        public DateTime AssignedDate { get; set; }
    }

    public class AssignmentDetailDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? PatientCardNumber { get; set; }
        public int PatientAge { get; set; }
        public string? PatientFirstName { get; set; }
        public string? PatientLastName { get; set; }
        public float Weight { get; set; }
        public float SystolicPressure { get; set; }
        public float DiastolicPressure { get; set; }
        public float RespiratoryRate { get; set; }
        public float PulseRate { get; set; }
        public float Temperature { get; set; }
        public float SpO2 { get; set; }
        public string? Triage { get; set; }
        public string? RespiratoryRateCategory { get; set; }
        public string? PulseRateCategory { get; set; }
        public string? BloodPressureCategory { get; set; }
        public string? TemperatureCategory { get; set; }
        public string? SpO2Category { get; set; }
        public DateTime AssignedDate { get; set; }
    }



    public class AssignDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
