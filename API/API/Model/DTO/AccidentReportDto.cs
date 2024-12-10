namespace API.Model.DTO
{
    public class AccidentReportDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientDTO Patient { get; set; }
        public string Department { get; set; }
        public DateTime InjuryDate { get; set; }
        public string InjuryTime { get; set; }
        public string CauseOfInjury { get; set; }
        public string IncidentDetails { get; set; }
        public string PreventiveAction { get; set; }
        public string ReporterName { get; set; }
        public string DepartmentHeadName { get; set; }
        public List<WitnessDto> Witnesses { get; set; }
    }
    public class AccidentReportCreateDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Department { get; set; }
        public DateTime InjuryDate { get; set; }
        public string InjuryTime { get; set; }
        public string CauseOfInjury { get; set; }
        public string IncidentDetails { get; set; }
        public string PreventiveAction { get; set; }
        public string ReporterName { get; set; }
        public string DepartmentHeadName { get; set; }
        public List<WitnessDto> Witnesses { get; set; }
    }


}
